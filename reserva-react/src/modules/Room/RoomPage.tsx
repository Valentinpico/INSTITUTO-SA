import { TableRoom } from "./components/TableRoom";

export const RoomPage = () => {
  return (
    <div>
      <h1 className="text-3xl uppercase text-slate-700 font-black px-5 mt-5">
        Rooms Admin
      </h1>
      <TableRoom />
    </div>
  );
};
