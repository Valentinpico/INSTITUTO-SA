import { ColumnDef } from "@tanstack/react-table";

import { DropDownActions } from "@/components/common/DropDownActions";
import { Movie } from "../schemas/MovieSchema";
import { BadgeStatus } from "@/components/common/BadgeStatus";
import { MovieGenreEnumLabel } from "../utils/enums";

export type ActionsDropTable = {
  editAction: (movie: Movie) => void;
  deleteAction: (movie: Movie) => void;
};

export const getColumsMovieTable = ({
  deleteAction,
  editAction,
}: ActionsDropTable) => {
  const columns: ColumnDef<Movie>[] = [
    {
      accessorKey: "id",
      header: "ID",
    },
    {
      accessorKey: "name",
      header: "Name",
    },
    {
      accessorKey: "genre",
      header: "Genre",
      cell: ({ row }) => {
        const genre = row.getValue("genre") as keyof typeof MovieGenreEnumLabel;

        return <span>{MovieGenreEnumLabel[genre]}</span>;
      },
    },
    {
      accessorKey: "allowedAge",
      header: "Allowed Age",
    },
    {
      accessorKey: "lengthMinutes",
      header: "Length Minutes",
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
