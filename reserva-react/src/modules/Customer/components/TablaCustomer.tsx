import { TablaDinamica } from "@/components/common/TablaDinamica";
import { getColumsCustomerTable } from "./ColumnsCustomerTable";
import { Button } from "@/components/ui/button";
import { ModalDefault } from "@/components/common/ModalComponent";
import { useEffect, useState } from "react";
import { FormCustomer } from "./FormCustomer";
import { Customer } from "../schemas/CustomerSchema";
import { deleteCustomer_api, getCustomers_api } from "../api/customer.service";
import { showToast } from "@/adapters/toast/handleToast";
import { useBookingContext } from "@/Context/BookingProvider";

export const TableCustomer = () => {
  const { modal, setModal, customerSelected, setCustomerSelected } =
    useBookingContext();

  const [data, setData] = useState<Customer[]>([]);
  const [modalEliminar, setModalEliminar] = useState(false);

  const getAllCustomer = async () => {
    const res = await getCustomers_api();

    if (!res.success) {
      showToast(res.message || "An error occurred", "error");
    }

    setData(res.data || []);
  };

  const handleEdit = (customer: Customer) => {
    setModal(true);
    setCustomerSelected(customer);
  };
  const ButtonDelete = (customer: Customer) => {
    setModalEliminar(true);
    setCustomerSelected(customer);
  };
  const columns = getColumsCustomerTable({
    deleteAction: ButtonDelete,
    editAction: handleEdit,
  });

  const handleCreate = () => {
    setModal(true);
    setCustomerSelected(null);
  };

  const handleDelete = async () => {
    const res = await deleteCustomer_api(customerSelected?.id || 0);

    showToast(
      res.message || "Customer deleted successfully",
      res.success ? "success" : "error"
    );

    getAllCustomer();
    setModalEliminar(false);
  };

  useEffect(() => {
    getAllCustomer();
    return () => {
      console.log("TableCustomer unmounted");
    };
  }, []);
  return (
    <div className="container mx-auto py-5">
      <Button
        variant={"outline"}
        className="mb-2 p-5 hover:cursor-pointer"
        onClick={handleCreate}
      >
        New Customer
      </Button>
      <TablaDinamica columns={columns} data={data} />
      <ModalDefault modal={modal} setModal={setModal}>
        <FormCustomer getAllcustomer={getAllCustomer} />
      </ModalDefault>

      <ModalDefault modal={modalEliminar} setModal={setModalEliminar}>
        <h1 className="text-2xl font-bold">Are you sure you want to delete?</h1>
        <p className="text-sm text-gray-500">This action cannot be undone</p>

        {/* customer info  */}
        <div className="space-y-1 border-t pt-4 mt-4">
          <p>
            <span className="font-semibold">Document Number:</span>
            {customerSelected?.documentNumber}
          </p>
          <p>
            <span className="font-semibold">Name:</span>
            {customerSelected?.name} {customerSelected?.lastname}
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
