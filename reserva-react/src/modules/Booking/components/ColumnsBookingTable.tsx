import { ColumnDef } from "@tanstack/react-table";
import { DropDownActions } from "@/components/common/DropDownActions";
import { Booking } from "../schemas/BookingSchema";
import { BadgeStatus } from "@/components/common/BadgeStatus";
import { formatDate } from "@/modules/Billboard/components/BillboardCard";

export type ActionsDropTable = {
  editAction: (booking: Booking) => void;
  deleteAction: (booking: Booking) => void;
};

export const getColumsBookingTable = ({
  deleteAction,
  editAction,
}: ActionsDropTable) => {
  const columns: ColumnDef<Booking>[] = [
    {
      accessorKey: "id",
      header: "ID",
    },

    {
      accessorKey: "date",
      header: "Date",
      cell: ({ row }) => {
        const date = row.original.date;
        return <span>{formatDate(new Date(date).toISOString( ))}</span>;
      },
    },
    {
      accessorKey: "customer",
      header: "User",
      cell: ({ row }) => {
        const customer = row.original.customer;
        return <span>{customer?.name}</span>;
      },
    },
    {
      accessorKey: "billboard",
      header: "Movie",
      cell: ({ row }) => {
        const billboard = row.original.billboard;
        return <span>{billboard?.movie?.name}</span>;
      },
    },

    {
      accessorKey: "billboard",
      header: "room",
      cell: ({ row }) => {
        const room = row.original?.billboard?.room;
        return <span>{room?.name}</span>;
      },
    },
    {
      accessorKey: "billboard",
      header: "Seat",
      cell: ({ row }) => {
        const seat = row.original.seat;
        return (
          <span>
            Colunm: {seat?.number} - Row: {seat?.rowNumber}
          </span>
        );
      },
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
        const booking = row.original;

        return (
          <DropDownActions
            deleteAction={() => deleteAction(booking)}
            editAction={() => editAction(booking)}
          />
        );
      },
    },
  ];

  return columns;
};
