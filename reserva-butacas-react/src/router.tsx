import { createBrowserRouter } from "react-router-dom";
import { Home } from "./Home";
import { Layout } from "./layouts/Layout";

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
        element: <Home />,
      },
    ],
  },
]);
