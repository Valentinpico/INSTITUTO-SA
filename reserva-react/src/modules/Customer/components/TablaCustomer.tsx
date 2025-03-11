import { TablaDinamica } from "@/components/common/TablaDinamica";
import { Customer } from "@/types/customer.type";
import { getColumsCustomerTable } from "./ColumnsCustomerTable";
import { Button } from "@/components/ui/button";
import { ModalDefault } from "@/components/common/ModalComponent";
import { useState } from "react";
import { FormCustomer } from "./FormCustomer";
const data: Customer[] = [
  {
    id: "1",
    name: "Zohn Doe",
    email: "john@example.com",
    status: true,
    age: 25,
    phoneNumber: "555-1234",
    documentNumber: "12345678",
    lastName: "Doe",
  },
  {
    id: "2",
    name: "Jane Doe",
    email: "@sdfdsf",
    status: false,
    age: 25,
    phoneNumber: "555-1234",
    documentNumber: "12345678",
    lastName: "Doe",
  },
  {
    id: "2",
    name: "Jane Doe",
    email: "@sdfdsf",
    status: false,
    age: 25,
    phoneNumber: "555-1234",
    documentNumber: "12345678",
    lastName: "Doe",
  },
  {
    id: "2",
    name: "Jane Doe",
    email: "@sdfdsf",
    status: false,
    age: 25,
    phoneNumber: "555-1234",
    documentNumber: "12345678",
    lastName: "Doe",
  },
];

export const TableCustomer = () => {
  const [modal, setModal] = useState(false);

  const handleEdit = (customer: Customer) => {
    alert(`Edit ${customer.name}`);
  };
  const handleDelete = (customer: Customer) => {
    alert(`Delete ${customer.name}`);
  };
  const columns = getColumsCustomerTable({
    deleteAction: handleDelete,
    editAction: handleEdit,
  });
  return (
    <div className="container mx-auto py-5">
      <Button
        variant={"outline"}
        className="mb-2 p-5 hover:cursor-pointer"
        onClick={() => setModal(true)}
      >
        New Customer
      </Button>
      <TablaDinamica columns={columns} data={data} />

      <ModalDefault modal={modal} setModal={setModal}>
        <h1>Modal</h1>
        <FormCustomer />
      </ModalDefault>
    </div>
  );
};
