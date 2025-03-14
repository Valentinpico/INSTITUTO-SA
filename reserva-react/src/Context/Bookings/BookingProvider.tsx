import { ReactNode, useContext, useState } from "react";
import BookingContext from "./BookingContext"; // Ajusta la ruta de importación según sea necesario
import { Customer } from "@/modules/Customer/schemas/CustomerSchema";
import { Room } from "@/modules/Room/schemas/RoomSchema";
import { Movie } from "@/modules/Movie/schemas/MovieSchema";
import { Seat } from "@/modules/Seat/schemas/SeatSchema";
import { Billboard } from "@/modules/Billboard/schemas/BillboardSchema";
import { Booking } from "@/modules/Booking/schemas/BookingSchema";
import { getCustomers_api } from "@/modules/Customer/api/customer.service";
import { getRooms_api } from "@/modules/Room/api/room.service";
import { getBookings_api } from "@/modules/Booking/api/booking.service";
import { getMovies_api } from "@/modules/Movie/api/movie.service";
import { getSeats_api } from "@/modules/Seat/api/seat.service";
import { getBillboards_api } from "@/modules/Billboard/api/billboard.service";

type BookingProviderProps = {
  children: ReactNode;
};

export const BookingProvider = ({ children }: BookingProviderProps) => {
  const [allCustomers, setAllCustomers] = useState<Customer[]>([]);
  const [allRooms, setAllRooms] = useState<Room[]>([]);
  const [allMovies, setAllMovies] = useState<Movie[]>([]);
  const [allSeats, setAllSeats] = useState<Seat[]>([]);
  const [allBillboards, setAllBillboards] = useState<Billboard[]>([]);
  const [allBookings, setAllBookings] = useState<Booking[]>([]);

  const getAllCustomers = async () => {
    const res = await getCustomers_api();
    setAllCustomers(res.data || []);
    return res;
  };

  const getAllRooms = async () => {
    const res = await getRooms_api();
    setAllRooms(res.data || []);
    return res;
  };

  const getAllMovies = async () => {
    const res = await getMovies_api();
    setAllMovies(res.data || []);
    return res;
  };

  const getAllSeats = async () => {
    const res = await getSeats_api();
    setAllSeats(res.data || []);
    return res;
  };

  const getAllBillboards = async () => {
    const res = await getBillboards_api();
    setAllBillboards(res.data || []);
    return res;
  };

  const getAllBookings = async () => {
    const res = await getBookings_api();
    setAllBookings(res.data || []);
    return res;
  };

  return (
    <BookingContext.Provider
      value={{
        allCustomers,
        allRooms,
        allMovies,
        allSeats,
        allBillboards,
        allBookings,
        getAllCustomers,
        getAllRooms,
        getAllMovies,
        getAllSeats,
        getAllBillboards,
        getAllBookings,
        setAllBookings,
      }}
    >
      {children}
    </BookingContext.Provider>
  );
};

export const useBookingContext = () => {
  const context = useContext(BookingContext);

  if (!context) {
    throw new Error("Error en el contexto de booking");
  }

  return context;
};
