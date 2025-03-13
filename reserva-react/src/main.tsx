import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./index.css";
import { Router } from "./router";
import { RouterProvider } from "react-router-dom";
import ToastAdapter from "./adapters/toast/ToastAdapter";
import { EntityProvider } from "./Context/Entities/EntityProvider";
import { BookingProvider } from "./Context/Bookings/BookingProvider";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <EntityProvider>
      <BookingProvider>
        <RouterProvider router={Router} />
      </BookingProvider>
    </EntityProvider>
    <ToastAdapter />
  </StrictMode>
);
