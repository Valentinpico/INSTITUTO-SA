import type { ColumnDef } from "@tanstack/react-table";
import { ArrowUpDown, MoreHorizontal } from "lucide-react";

import { Button } from "@/components/ui/button";
import { Checkbox } from "@/components/ui/checkbox";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { Badge } from "@/components/ui/badge";

import { TablaDinamica } from "../components/common/TablaDinamica";
import { Customer } from "@/types/customer.type";

// Sample data
const data: Customer[] = [
  {
    id: "1",
    name: "John Doe",
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

export const Home = () => {
  // const [selectedUsers, setSelectedUsers] = useState<string[]>([]);

  // Define columns with custom rendering
  const columns: ColumnDef<Customer>[] = [
    {
      id: "select",
      header: ({ table }) => (
        <Checkbox
          checked={
            table.getIsAllPageRowsSelected() ||
            (table.getIsSomePageRowsSelected() && "indeterminate")
          }
          onCheckedChange={(value) => table.toggleAllPageRowsSelected(!!value)}
          aria-label="Select all"
        />
      ),
      cell: ({ row }) => (
        <Checkbox
          checked={row.getIsSelected()}
          onCheckedChange={(value) => row.toggleSelected(!!value)}
          aria-label="Select row"
        />
      ),
      enableSorting: false,
      enableHiding: false,
    },
    {
      accessorKey: "name",
      header: ({ column }) => {
        return (
          <Button
            variant="ghost"
            onClick={() => column.toggleSorting(column.getIsSorted() === "asc")}
          >
            Name
            <ArrowUpDown className="ml-2 h-4 w-4" />
          </Button>
        );
      },
      cell: ({ row }) => (
        <div className="font-medium">{row.getValue("name")}</div>
      ),
    },
    {
      accessorKey: "email",
      header: "Email",
    },
    {
      accessorKey: "status",
      header: "Status",
      cell: ({ row }) => {
        const status = row.getValue("status") as string;

        return (
          <Badge
            variant={
              status === "active"
                ? "default"
                : status === "inactive"
                ? "secondary"
                : "outline"
            }
          >
            {status}
          </Badge>
        );
      },
    },
    {
      accessorKey: "role",
      header: "Role",
    },
    {
      id: "actions",
      cell: ({ row }) => {
        const user = row.original;

        return (
          <DropdownMenu>
            <DropdownMenuTrigger asChild>
              <Button variant="ghost" className="h-8 w-8 p-0">
                <span className="sr-only">Open menu</span>
                <MoreHorizontal className="h-4 w-4" />
              </Button>
            </DropdownMenuTrigger>
            <DropdownMenuContent align="end">
              <DropdownMenuLabel>Actions</DropdownMenuLabel>
              <DropdownMenuItem
                onClick={() => navigator.clipboard.writeText(user.id)}
              >
                Copy user ID
              </DropdownMenuItem>
              <DropdownMenuSeparator />
              <DropdownMenuItem>View user details</DropdownMenuItem>
              <DropdownMenuItem>Edit user</DropdownMenuItem>
              <DropdownMenuItem className="text-destructive">
                Delete user
              </DropdownMenuItem>
            </DropdownMenuContent>
          </DropdownMenu>
        );
      },
    },
  ];

  return (
    <div className="container mx-auto py-10">
      <TablaDinamica columns={columns} data={data} />
    </div>
  );
};
