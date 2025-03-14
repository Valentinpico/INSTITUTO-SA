import { TablaDinamica } from "@/components/common/TablaDinamica";
import { getColumsSeatTable } from "./ColumnsSeatTable";
import { Button } from "@/components/ui/button";
import { ModalDefault } from "@/components/common/ModalComponent";
import { useEffect, useState } from "react";
import { FormSeat } from "./FormSeat";
import { Seat } from "../schemas/SeatSchema";
import { deleteSeat_api } from "../api/seat.service";
import { showToast } from "@/adapters/toast/handleToast";
import { useEntityContext } from "@/Context/Entities/EntityProvider";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { optionsToSelect } from "../utils/optionsSelects";
import { useBookingContext } from "@/Context/Bookings/BookingProvider";

export const TableSeat = () => {
  const { modal, setModal, roomSelected, setSeatSelected, seatSelected } =
    useEntityContext();

  const [filter, setFilter] = useState(0);
  const [modalEliminar, setModalEliminar] = useState(false);
  const { allSeats, getAllSeats, getAllRooms, allRooms } = useBookingContext();

  const handleEdit = (room: Seat) => {
    setModal(true);
    setSeatSelected(room);
  };
  const ButtonDelete = (room: Seat) => {
    setModalEliminar(true);
    setSeatSelected(room);
  };
  const columns = getColumsSeatTable({
    deleteAction: ButtonDelete,
    editAction: handleEdit,
  });

  const handleCreate = () => {
    console.log(seatSelected);
    setModal(true);
    setSeatSelected(null);
  };

  const handleDelete = async () => {
    const res = await deleteSeat_api(roomSelected?.id || 0);

    showToast(
      res.message || "Seat deleted successfully",
      res.success ? "success" : "error"
    );

    getAllSeats();
    setModalEliminar(false);
  };

  const dataFiltered = allSeats.filter((seat) =>
    filter === 0 ? true : seat.roomID === filter
  );

  const optionsRoomsToSelect = optionsToSelect(allRooms);

  useEffect(() => {
    getAllSeats();
    getAllRooms();
  }, []);
  return (
    <div className="container mx-auto py-5">
      <div className="flex justify-between items-center">
        <Button
          variant={"outline"}
          className="mb-2 p-5 hover:cursor-pointer"
          onClick={handleCreate}
        >
          New Seat
        </Button>

        <Select>
          <Select
            onValueChange={(e) => setFilter(Number(e))}
            value={filter.toString()}
          >
            <SelectTrigger className="mt-1">
              <SelectValue placeholder="Select a room" />
            </SelectTrigger>
            <SelectContent>
              <SelectItem value="0">All rooms</SelectItem>
              {optionsRoomsToSelect.map((option) => (
                <SelectItem key={option.value} value={option.value.toString()}>
                  {option.label}
                </SelectItem>
              ))}
            </SelectContent>
          </Select>
        </Select>
      </div>

      <TablaDinamica columns={columns} data={dataFiltered} />

      <ModalDefault modal={modal} setModal={setModal}>
        <FormSeat />
      </ModalDefault>

      <ModalDefault modal={modalEliminar} setModal={setModalEliminar}>
        <h1 className="text-2xl font-bold">Are you sure you want to delete?</h1>
        <p className="text-sm text-gray-500">This action cannot be undone</p>

        <div className="space-y-1 border-t pt-4 mt-4">
          <p>
            <span className="font-semibold">Document Number:</span>
            {roomSelected?.number}
          </p>
          <p>
            <span className="font-semibold">Name:</span>
            {roomSelected?.name}
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
