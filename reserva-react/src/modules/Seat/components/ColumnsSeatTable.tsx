import { ColumnDef } from "@tanstack/react-table";
import { DropDownActions } from "@/components/common/DropDownActions";
import { Seat } from "../schemas/SeatSchema";
import { BadgeStatus } from "@/components/common/BadgeStatus";

export type ActionsDropTable = {
  editAction: (seat: Seat) => void;
  deleteAction: (seat: Seat) => void;
};

export const getColumsSeatTable = ({
  deleteAction,
  editAction,
}: ActionsDropTable) => {
  const columns: ColumnDef<Seat>[] = [
    {
      accessorKey: "id",
      header: "ID",
    },

    {
      accessorKey: "number",
      header: "Number",
    },

    {
      accessorKey: "rowNumber",
      header: "Number Row",
    },
    {
      accessorKey: "roomID",
      header: "Room",
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
        const seat = row.original;

        return (
          <DropDownActions
            deleteAction={() => deleteAction(seat)}
            editAction={() => editAction(seat)}
          />
        );
      },
    },
  ];

  return columns;
};
