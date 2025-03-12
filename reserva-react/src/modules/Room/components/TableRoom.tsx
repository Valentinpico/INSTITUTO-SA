import { TablaDinamica } from "@/components/common/TablaDinamica";
import { getColumsRoomTable } from "./ColumnsRoomTable";
import { Button } from "@/components/ui/button";
import { ModalDefault } from "@/components/common/ModalComponent";
import { useEffect, useState } from "react";
import { FormRoom } from "./FormRoom";
import { Room } from "../schemas/RoomSchema";
import { getRooms_api, deleteRoom_api } from "../api/room.service";
import { showToast } from "@/adapters/toast/handleToast";
import { useBookingContext } from "@/Context/BookingProvider";

export const TableRoom = () => {
  const { modal, setModal, roomSelected, setRoomSelected } =
    useBookingContext();

  const [data, setData] = useState<Room[]>([]);
  const [modalEliminar, setModalEliminar] = useState(false);

  const getAllRooms = async () => {
    const res = await getRooms_api();

    if (!res.success) {
      showToast(res.message || "An error occurred", "error");
    }

    setData(res.data || []);
  };

  const handleEdit = (room: Room) => {
    setModal(true);
    setRoomSelected(room);
  };
  const ButtonDelete = (room: Room) => {
    setModalEliminar(true);
    setRoomSelected(room);
  };
  const columns = getColumsRoomTable({
    deleteAction: ButtonDelete,
    editAction: handleEdit,
  });

  const handleCreate = () => {
    setModal(true);
    setRoomSelected(null);
  };

  const handleDelete = async () => {
    const res = await deleteRoom_api(roomSelected?.id || 0);

    showToast(
      res.message || "Room deleted successfully",
      res.success ? "success" : "error"
    );

    getAllRooms();
    setModalEliminar(false);
  };

  useEffect(() => {
    getAllRooms();
  }, []);
  return (
    <div className="container mx-auto py-5">
      <Button
        variant={"outline"}
        className="mb-2 p-5 hover:cursor-pointer"
        onClick={handleCreate}
      >
        New Room
      </Button>
      <TablaDinamica columns={columns} data={data} />

      <ModalDefault modal={modal} setModal={setModal}>
        <FormRoom getAllRooms={getAllRooms} />
      </ModalDefault>

      <ModalDefault modal={modalEliminar} setModal={setModalEliminar}>
        <h1 className="text-2xl font-bold">Are you sure you want to delete?</h1>
        <p className="text-sm text-gray-500">This action cannot be undone</p>

        {/* room info  */}
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
