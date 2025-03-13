import { createBrowserRouter } from "react-router-dom";
import { Home } from "./Home";
import { Layout } from "./layouts/Layout";
import { BillboardPage } from "./modules/Billboard/AdminCartelera";
import { BookingPage } from "./modules/Booking/ReservationList";
import { CustomerPage } from "./modules/Customer/CustomerPage";
import { MoviePage } from "./modules/Movie/MoviePage";
import { RoomPage } from "./modules/Room/RoomPage";
import { SeatPage } from "./modules/Seat/AdminButacas";

export const Router = createBrowserRouter([
  {
    path: "/",
    element: <Layout />,
    children: [
      {
        index: true,
        element: <Home />,
      },
      {
        path: "/billboard",
        element: <BillboardPage />,
      },
      {
        path: "/booking",
        element: <BookingPage />,
      },
      {
        path: "/customer",
        element: <CustomerPage />,
      },
      {
        path: "/movie",
        element: <MoviePage />,
      },
      {
        path: "/room",
        element: <RoomPage />,
      },
      {
        path: "/seat",
        element: <SeatPage />,
      },
    ],
  },
]);
