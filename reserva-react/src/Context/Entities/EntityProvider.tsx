import { ReactNode, useContext, useState } from "react";
import EntityContext from "./EntityContext"; // Ajusta la ruta de importación según sea necesario
import { Customer } from "@/modules/Customer/schemas/CustomerSchema";
import { Room } from "@/modules/Room/schemas/RoomSchema";
import { Movie } from "@/modules/Movie/schemas/MovieSchema";
import { Seat } from "@/modules/Seat/schemas/SeatSchema";
import { Billboard } from "@/modules/Billboard/schemas/BillboardSchema";
import { Booking } from "@/modules/Booking/schemas/BookingSchema";

type EntityProviderProps = {
  children: ReactNode;
};

export const EntityProvider = ({ children }: EntityProviderProps) => {
  const [modal, setModal] = useState(false);
  const [customerSelected, setCustomerSelected] = useState<Customer | null>(
    null
  );
  const [billboardSelected, setBillboardSelected] = useState<Billboard | null>(
    null
  );
  const [seatSelected, setSeatSelected] = useState<Seat | null>(null);
  const [roomSelected, setRoomSelected] = useState<Room | null>(null);
  const [movieSelected, setMovieSelected] = useState<Movie | null>(null);
  const [bookingSelected, setBookingSelected] = useState<Booking | null>(null);

  return (
    <EntityContext.Provider
      value={{
        modal,
        setModal,
        customerSelected,
        setCustomerSelected,
        roomSelected,
        setRoomSelected,
        movieSelected,
        setMovieSelected,
        seatSelected,
        setSeatSelected,
        billboardSelected,
        setBillboardSelected,
        bookingSelected,
        setBookingSelected,
      }}
    >
      {children}
    </EntityContext.Provider>
  );
};

export const useEntityContext = () => {
  const context = useContext(EntityContext);

  if (!context) {
    throw new Error("Error en el contexto de booking");
  }

  return context;
};
