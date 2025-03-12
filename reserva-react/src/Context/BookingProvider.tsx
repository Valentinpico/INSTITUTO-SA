import { ReactNode, useContext, useState } from "react";
import BookingContext from "./BookingContext"; // Ajusta la ruta de importación según sea necesario
import { Customer } from "@/modules/Customer/schemas/CustomerSchema";
import { Room } from "@/modules/Room/schemas/RoomSchema";
import { Movie } from "@/modules/Movie/schemas/MovieSchema";

type BookingProviderProps = {
  children: ReactNode;
};

export const BookingProvider = ({ children }: BookingProviderProps) => {
  const [modal, setModal] = useState(false);
  const [customerSelected, setCustomerSelected] = useState<Customer | null>(
    null
  );
  const [roomSelected, setRoomSelected] = useState<Room | null>(null);
  const [movieSelected, setMovieSelected] = useState<Movie | null>(null);
  /*   const [bookingSelected, setBookingSelected] = useState<Booking | null>(null);
  const [billboardSelected, setBillboardSelected] = useState<Billboard | null>(
    null
  );
  const [seatSelected, setSeatSelected] = useState<Seat | null>(null);
 */
  return (
    <BookingContext.Provider
      value={{
        modal,
        setModal,
        customerSelected,
        setCustomerSelected,
        roomSelected,
        setRoomSelected,
        movieSelected,
        setMovieSelected,
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
