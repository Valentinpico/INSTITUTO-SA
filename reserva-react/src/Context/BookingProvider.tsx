import { ReactNode, useContext, useState } from "react";
import BookingContext from "./BookingContext"; // Ajusta la ruta de importación según sea necesario
import { Customer } from "@/modules/Customer/schemas/CustomerSchema";
import { Room } from "@/modules/Room/schemas/RoomSchema";

type BookingProviderProps = {
  children: ReactNode;
};

export const BookingProvider = ({ children }: BookingProviderProps) => {
  const [modal, setModal] = useState(false);
  const [customerSelected, setCustomerSelected] = useState<Customer | null>(
    null
  );
  const [roomSelected, setRoomSelected] = useState<Room | null>(null);

  return (
    <BookingContext.Provider
      value={{
        modal,
        setModal,
        customerSelected,
        setCustomerSelected,
        roomSelected,
        setRoomSelected,
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
