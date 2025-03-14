import { TablaDinamica } from "@/components/common/TablaDinamica";
import { getColumsBookingTable } from "./ColumnsBookingTable";
import { Button } from "@/components/ui/button";
import { ModalDefault } from "@/components/common/ModalComponent";
import { useEffect, useState } from "react";
import { Booking } from "../schemas/BookingSchema";
import { deleteBooking_api } from "../api/booking.service";
import { showToast } from "@/adapters/toast/handleToast";
import { useEntityContext } from "@/Context/Entities/EntityProvider";
import { useBookingContext } from "@/Context/Bookings/BookingProvider";
import { FormBooking } from "./FormBooking";

export const TableBooking = () => {
  const { modal, setModal, roomSelected, setBookingSelected, seatSelected } =
    useEntityContext();

  const [modalEliminar, setModalEliminar] = useState(false);
  const { getAllBookings, allBookings } = useBookingContext();

  const handleEdit = (room: Booking) => {
    setModal(true);
    setBookingSelected(room);
  };
  const ButtonDelete = (room: Booking) => {
    setModalEliminar(true);
    setBookingSelected(room);
  };
  const columns = getColumsBookingTable({
    deleteAction: ButtonDelete,
    editAction: handleEdit,
  });

  const handleCreate = () => {
    console.log(seatSelected);
    setModal(true);
    setBookingSelected(null);
  };

  const handleDelete = async () => {
    const res = await deleteBooking_api(roomSelected?.id || 0);

    showToast(
      res.message || "Booking deleted successfully",
      res.success ? "success" : "error"
    );

    getAllBookings();
    setModalEliminar(false);
  };

  useEffect(() => {
    getAllBookings();
  }, []);
  return (
    <div className="container mx-auto py-5">
      <div className="flex justify-between items-center">
        <Button
          variant={"outline"}
          className="mb-2 p-5 hover:cursor-pointer"
          onClick={handleCreate}
        >
          New Booking
        </Button>

        {/*  <Select>
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
            </SelectContent∏
          </Select>
        </Select> */}
      </div>

      <TablaDinamica columns={columns} data={allBookings} />

      <ModalDefault modal={modal} setModal={setModal}>
        <FormBooking />
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
