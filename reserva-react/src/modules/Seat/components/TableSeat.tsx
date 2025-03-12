import { TablaDinamica } from "@/components/common/TablaDinamica";
import { getColumsSeatTable } from "./ColumnsSeatTable";
import { Button } from "@/components/ui/button";
import { ModalDefault } from "@/components/common/ModalComponent";
import { useEffect, useState } from "react";
import { FormSeat } from "./FormSeat";
import { Seat } from "../schemas/SeatSchema";
import { getSeats_api, deleteSeat_api } from "../api/seat.service";
import { showToast } from "@/adapters/toast/handleToast";
import { useBookingContext } from "@/Context/BookingProvider";

export const TableSeat = () => {
  const { modal, setModal, roomSelected, setSeatSelected, seatSelected } =
    useBookingContext();

  const [data, setData] = useState<Seat[]>([]);
  const [modalEliminar, setModalEliminar] = useState(false);

  const getAllSeats = async () => {
    const res = await getSeats_api();

    if (!res.success) {
      showToast(res.message || "An error occurred", "error");
    }

    setData(res.data || []);
  };

  const handleEdit = (room: Seat) => {
    setModal(true);
    setSeatSelected(room);
  };
  const ButtonDelete = (room: Seat) => {
    setModalEliminar(true);
    setSeatSelected(room);
  };
  const columns = getColumsSeatTable({
    deleteAction: ButtonDelete,
    editAction: handleEdit,
  });

  const handleCreate = () => {
    console.log(seatSelected);
    setModal(true);
    setSeatSelected(null);
  };

  const handleDelete = async () => {
    const res = await deleteSeat_api(roomSelected?.id || 0);

    showToast(
      res.message || "Seat deleted successfully",
      res.success ? "success" : "error"
    );

    getAllSeats();
    setModalEliminar(false);
  };

  useEffect(() => {
    getAllSeats();
  }, []);
  return (
    <div className="container mx-auto py-5">
      <Button
        variant={"outline"}
        className="mb-2 p-5 hover:cursor-pointer"
        onClick={handleCreate}
      >
        New Seat
      </Button>
      <TablaDinamica columns={columns} data={data} />

      <ModalDefault modal={modal} setModal={setModal}>
        <FormSeat getAllSeats={getAllSeats} />
      </ModalDefault>

      <ModalDefault modal={modalEliminar} setModal={setModalEliminar}>
        <h1 className="text-2xl font-bold">Are you sure you want to delete?</h1>
        <p className="text-sm text-gray-500">This action cannot be undone</p>

        <div className="space-y-1 border-t pt-4 mt-4">
          <p>
            <span className="font-semibold">Document Number:</span>
            {roomSelected?.number}
          </p>
          <p>
            <span className="font-semibold">Name:</span>
            {roomSelected?.name}
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
