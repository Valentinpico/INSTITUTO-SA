import { ColumnDef } from "@tanstack/react-table";
import { DropDownActions } from "@/components/common/DropDownActions";
import { Seat } from "../schemas/SeatSchema";
import { BadgeStatus } from "@/components/common/BadgeStatus";
import { Button } from "@/components/ui/button";

export type ActionsDropTable = {
  editAction: (seat: Seat) => void;
  deleteAction: (seat: Seat) => void;
  disableAction: (seat: Seat) => void;
};

export const getColumsSeatTable = ({
  deleteAction,
  editAction,
  disableAction,
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
      cell: ({ row }) => {
        const id = row.getValue("roomID") as number;

        return <span>{id}</span>;
      },
    },

    {
      accessorKey: "status",
      header: "Status",
      cell: ({ row }) => <BadgeStatus status={row.getValue("status")} />,
    },
    {
      accessorKey: "status",
      header: "Disabled",
      cell: ({ row }) => (
        <Button variant="outline" disabled={!row.original.status} onClick={() => disableAction(row.original)}>Disable</Button>
      ),
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
