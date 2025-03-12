import { Customer } from "@/modules/Customer/schemas/CustomerSchema";
import { createContext } from "react";

type BookingContextType = {
  modal: boolean;
  setModal: (value: boolean) => void;
  customerSelected: Customer | null;
  setCustomerSelected: (value: Customer | null) => void;
};

const BookingContext = createContext<BookingContextType>(
  {} as BookingContextType
);

export default BookingContext;
