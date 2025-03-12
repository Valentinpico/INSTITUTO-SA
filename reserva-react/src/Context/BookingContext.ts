import { Customer } from "@/modules/Customer/schemas/CustomerSchema";
import { Room } from "@/modules/Room/schemas/RoomSchema";
import { createContext } from "react";

type BookingContextType = {
  modal: boolean;
  setModal: (value: boolean) => void;
  customerSelected: Customer | null;
  setCustomerSelected: (value: Customer | null) => void;
  roomSelected: Room | null;
  setRoomSelected: (value: Room | null) => void;
};

const BookingContext = createContext<BookingContextType>(
  {} as BookingContextType
);

export default BookingContext;
