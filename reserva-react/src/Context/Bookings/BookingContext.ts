import { Billboard } from "@/modules/Billboard/schemas/BillboardSchema";
import { Booking } from "@/modules/Booking/schemas/BookingSchema";
import { Customer } from "@/modules/Customer/schemas/CustomerSchema";
import { Movie } from "@/modules/Movie/schemas/MovieSchema";
import { Room } from "@/modules/Room/schemas/RoomSchema";
import { Seat } from "@/modules/Seat/schemas/SeatSchema";
import { createContext } from "react";

type EntityContextType = {
  allCustomers: Customer[];
  allRooms: Room[];
  allMovies: Movie[];
  allSeats: Seat[];
  allBillboards: Billboard[];
  allBookings: Booking[];
  getAllCustomers: () => void;
  getAllRooms: () => void;
  getAllMovies: () => void;
  getAllSeats: () => void
  getAllBillboards: () => void;
  getAllBookings: () => void;
};

const EntityContext = createContext<EntityContextType>({} as EntityContextType);

export default EntityContext;
