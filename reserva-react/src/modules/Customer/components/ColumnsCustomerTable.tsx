import { ColumnDef } from "@tanstack/react-table";

import { Customer } from "@/types/customer.type";
import { DropDownActions } from "@/components/common/DropDownActions";

export type ActionsDropTable = {
  editAction: (customer: Customer) => void;
  deleteAction: (customer: Customer) => void;
};

export const getColumsCustomerTable = ({
  deleteAction,
  editAction,
}: ActionsDropTable) => {
  const columns: ColumnDef<Customer>[] = [
    {
      accessorKey: "documentNumber",
      header: "Nro. Documento",
    },
    {
      accessorKey: "name",
      header: "Name",
    },
    {
      accessorKey: "lastName",
      header: "Last Name",
    },
    {
      accessorKey: "age",
      header: "Age",
    },
    {
      accessorKey: "email",
      header: "Email",
    },
    {
      accessorKey: "phoneNumber",
      header: "Phone Number",
    },

    {
      accessorKey: "status",
      header: "Status",
      cell: ({ row }) => (
        <span
          className={`badge  ${
            row.getValue("status")
              ? "bg-green-600 border-1 border-green-700"
              : "bg-red-500"
          } text-white p-1 rounded font-bold`}
        >
          {row.getValue("status") ? "Activo" : "No Activo"}
        </span>
      ),
    },
    {
      id: "actions",
      cell: ({ row }) => {
        const user = row.original;

        return (
          <DropDownActions
            deleteAction={() => deleteAction(user)}
            editAction={() => editAction(user)}
          />
        );
      },
    },
  ];

  return columns;
};
