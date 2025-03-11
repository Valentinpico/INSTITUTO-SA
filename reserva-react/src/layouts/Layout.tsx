import { Link, Outlet } from "react-router-dom";
import { columsNav } from "../common/utils/contanst";

export const Layout = () => {
  return (
    <div>
      <nav className="bg-blue-400 p-4 text-2xl text-white  flex justify-between items-center">
        <div className="uppercase">Cine Valentin Pico</div>

        <section className="flex  gap-5">
          {columsNav.map((item, index) => {
            return (
              <div
                className="hover:bg-blue-500 p-2 rounded transition"
                key={index}
              >
                <Link key={index} className="navbar-brand" to={item.path}>
                  {item.name}
                </Link>
              </div>
            );
          })}
        </section>
      </nav>

      <Outlet />
    </div>
  );
};
