import { Link, Outlet } from "react-router-dom";
import { columsNav } from "../utils/contanst";

export const Layout = () => {
  return (
    <div>
      <nav className="bg-blue-400 p-4 text-2xl text-white  flex justify-between items-center">
        <div className="uppercase">Cine Valentin Pico</div>

        <section className="flex  gap-5">
          {columsNav.map((item, index) => {
            return (
           
                <Link key={index} className="navbar-brand hover:bg-blue-500 rounded transition hover:cursor-pointer p-2" to={item.path}>
                  {item.name}
                </Link>
             
            );
          })}
        </section>
      </nav>

      <Outlet />
    </div>
  );
};
