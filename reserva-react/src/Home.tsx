import { useEffect, useState } from "react";
import { useBookingContext } from "./Context/Bookings/BookingProvider";
import { BillboardCard } from "./modules/Billboard/components/BillboardCard";
import { getSeatsAvailabilityToday_api } from "./modules/Seat/api/seat.service";
import { SeatsAvailableOrOccupied } from "./modules/Seat/schemas/SeatSchema";

export const Home = () => {
  const { allBillboards, getAllBillboards } = useBookingContext();

  const [seatsInfo, setSeatsInfo] = useState<SeatsAvailableOrOccupied[]>([]);

  const billboardsToday = allBillboards.filter((billboard) => {
    const today = new Date();
    const billboardDate = new Date(billboard.date);
    return billboardDate.getDate() === today.getDate();
  });

  const getAvailableSeats = async () => {
    const seatsInfo = await getSeatsAvailabilityToday_api();

    setSeatsInfo(seatsInfo.data || []);
  };

  useEffect(() => {
    getAvailableSeats();
    getAllBillboards();
  }, []);

  return (
    <div className="container mx-auto py-10">
      <h1 className="text-2xl uppercase text-slate-700 font-black px-5 mt-5">
        Seats information today
      </h1>

      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-5 p-5">
        {seatsInfo.map((billboard) => (
          <div
            className="bg-white shadow-md rounded-md p-5"
            key={billboard.roomID}
          >
            <h2 className="text-xl font-bold text-slate-700">
              Room: {billboard.room?.name}
            </h2>
            <p>
              Available: {billboard.available} | Occupied: {billboard.occupied}{" "}
              | Total: {billboard.total}
            </p>
          </div>
        ))}
      </div>

      <h1 className="text-2xl uppercase text-slate-700 font-black px-5 mt-5">
        Today's Billboards
      </h1>
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-5 p-5">
        {billboardsToday.map((billboard) => (
          <BillboardCard billboard={billboard} key={billboard.id} home />
        ))}
      </div>
    </div>
  );
};
