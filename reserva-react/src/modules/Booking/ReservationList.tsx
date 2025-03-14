import { TableBooking } from "./components/TableBooking";

export const BookingPage = () => {
  return (
    <div>
      <h1 className="text-3xl uppercase text-slate-700 font-black px-5 mt-5">
        Bookings Admin
      </h1>
      <TableBooking />
    </div>
  );
};
