import { Button } from "@/components/ui/button";
import { ModalDefault } from "@/components/common/ModalComponent";
import { useEffect, useState } from "react";
import { FormBillboard } from "./FormBillboard";
import { Billboard } from "../schemas/BillboardSchema";
import { deleteBillboard_api } from "../api/billboard.service";
import { showToast } from "@/adapters/toast/handleToast";
import { useEntityContext } from "@/Context/Entities/EntityProvider";
import { BillboardCard } from "./BillboardCard";
import { useBookingContext } from "@/Context/Bookings/BookingProvider";

export const TableBillboard = () => {
  const { modal, setModal, billboardSelected, setBillboardSelected } =
    useEntityContext();

  const [modalEliminar, setModalEliminar] = useState(false);

  const { getAllBillboards, allBillboards } = useBookingContext();

  const buttonDelete = (customer: Billboard) => {
    setModalEliminar(true);
    setBillboardSelected(customer);
  };

  const handleCreate = () => {
    setModal(true);
    setBillboardSelected(null);
  };

  const handleDelete = async () => {
    const res = await deleteBillboard_api(billboardSelected?.id || 0);

    showToast(
      res.message || "Billboard deleted successfully",
      res.success ? "success" : "error"
    );

    getAllBillboards();
    setModalEliminar(false);
  };

  useEffect(() => {
    getAllBillboards();
  }, [getAllBillboards]);
  return (
    <div className="container mx-auto py-5">
      <Button
        variant={"outline"}
        className="mb-2 p-5 hover:cursor-pointer"
        onClick={handleCreate}
      >
        New Billboard
      </Button>

      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-4">
        {allBillboards.length > 0 ? (
          allBillboards.map((billboard) => (
            <BillboardCard
              key={billboard.id}
              billboard={billboard}
              buttonDelete={buttonDelete}
            />
          ))
        ) : (
          <p>No billboards found</p>
        )}
      </div>

      <ModalDefault modal={modal} setModal={setModal}>
        <FormBillboard />
      </ModalDefault>

      <ModalDefault modal={modalEliminar} setModal={setModalEliminar}>
        <h1 className="text-2xl font-bold">Are you sure you want to delete?</h1>
        <p className="text-sm text-gray-500">This action cannot be undone</p>

        <div className="space-y-1 border-t pt-4 mt-4">
          <p>
            <span className="font-semibold">ID:</span>
            {billboardSelected?.id}
          </p>
          <p>
            <span className="font-semibold">estado:</span>
            {billboardSelected?.status}
          </p>
        </div>
        <div className="flex justify-end space-x-2 border-t pt-4 mt-4">
          <Button variant="outline" onClick={() => setModalEliminar(false)}>
            Cancel
          </Button>
          <Button variant="destructive" onClick={handleDelete}>
            Delete
          </Button>
        </div>
      </ModalDefault>
    </div>
  );
};
