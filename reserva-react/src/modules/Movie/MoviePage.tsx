import { TableMovie } from "./components/TablaMovie";

export const MoviePage = () => {
  return (
    <div>
      <h1 className="text-3xl uppercase text-slate-700 font-black px-5 mt-5">
        Customers Admin
      </h1>

      <TableMovie />
    </div>
  );
};
