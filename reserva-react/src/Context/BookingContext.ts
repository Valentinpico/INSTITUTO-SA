import { Billboard } from "@/modules/Billboard/schemas/BillboardSchema";
import { Customer } from "@/modules/Customer/schemas/CustomerSchema";
import { Movie } from "@/modules/Movie/schemas/MovieSchema";
import { Room } from "@/modules/Room/schemas/RoomSchema";
import { Seat } from "@/modules/Seat/schemas/SeatSchema";
import { createContext } from "react";

type BookingContextType = {
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
};

const BookingContext = createContext<BookingContextType>(
  {} as BookingContextType
);

export default BookingContext;
