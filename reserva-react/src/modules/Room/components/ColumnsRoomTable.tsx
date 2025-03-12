import { ColumnDef } from "@tanstack/react-table";
import { DropDownActions } from "@/components/common/DropDownActions";
import { Room } from "../schemas/RoomSchema";
import { BadgeStatus } from "@/components/common/BadgeStatus";

export type ActionsDropTable = {
  editAction: (room: Room) => void;
  deleteAction: (room: Room) => void;
};

export const getColumsRoomTable = ({
  deleteAction,
  editAction,
}: ActionsDropTable) => {
  const columns: ColumnDef<Room>[] = [
    {
      accessorKey: "id",
      header: "ID",
    },

    {
      accessorKey: "name",
      header: "Name",
    },

    {
      accessorKey: "number",
      header: "Number",
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
        const room = row.original;

        return (
          <DropDownActions
            deleteAction={() => deleteAction(room)}
            editAction={() => editAction(room)}
          />
        );
      },
    },
  ];

  return columns;
};
