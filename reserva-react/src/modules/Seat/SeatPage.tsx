import { TableSeat } from "./components/TableSeat";

export const SeatPage = () => {
  return (
    <div>
      <h1 className="text-3xl uppercase text-slate-700 font-black px-5 mt-5">
        Seats Admin
      </h1>
      <TableSeat />
    </div>
  );
};
