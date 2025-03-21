import { ColumnDef } from "@tanstack/react-table";

import { DropDownActions } from "@/components/common/DropDownActions";
import { Customer } from "../schemas/CustomerSchema";
import { BadgeStatus } from "@/components/common/BadgeStatus";

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
      accessorKey: "id",
      header: "ID",
    },
    {
      accessorKey: "documentNumber",
      header: "Nro. Documento",
    },
    {
      accessorKey: "name",
      header: "Name",
    },
    {
      accessorKey: "lastname",
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
      cell: ({ row }) => <BadgeStatus status={row.getValue("status")} />,
    },
    {
      id: "actions",
      header: "Actions",
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
