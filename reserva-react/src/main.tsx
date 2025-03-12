import { StrictMode } from "react";
import { createRoot } from "react-dom/client";
import "./index.css";
import { Router } from "./router";
import { RouterProvider } from "react-router-dom";
import ToastAdapter from "./adapters/toast/ToastAdapter";
import { BookingProvider } from "./Context/BookingProvider";

createRoot(document.getElementById("root")!).render(
  <StrictMode>
    <BookingProvider>
      <RouterProvider router={Router} />
    </BookingProvider>
    <ToastAdapter />
  </StrictMode>
);
