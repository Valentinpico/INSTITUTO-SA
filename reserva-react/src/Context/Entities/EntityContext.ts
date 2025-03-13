import { Billboard } from "@/modules/Billboard/schemas/BillboardSchema";
import { Booking } from "@/modules/Booking/schemas/BookingSchema";
import { Customer } from "@/modules/Customer/schemas/CustomerSchema";
import { Movie } from "@/modules/Movie/schemas/MovieSchema";
import { Room } from "@/modules/Room/schemas/RoomSchema";
import { Seat } from "@/modules/Seat/schemas/SeatSchema";
import { createContext } from "react";

type EntityContextType = {
  modal: boolean;
  setModal: (value: boolean) => void;
  customerSelected: Customer | null;
  setCustomerSelected: (value: Customer | null) => void;
  roomSelected: Room | null;
  setRoomSelected: (value: Room | null) => void;
  movieSelected: Movie | null;
  setMovieSelected: (value: Movie | null) => void;
  seatSelected: Seat | null;
  setSeatSelected: (value: Seat | null) => void;
  billboardSelected: Billboard | null;
  setBillboardSelected: (value: Billboard | null) => void;
  bookingSelected: Booking | null;
  setBookingSelected: (value: Booking | null) => void;

};

const EntityContext = createContext<EntityContextType>(
  {} as EntityContextType
);

export default EntityContext;
