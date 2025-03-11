import { TableCustomer } from "./components/TablaCustomer";

export const CustomerPage = () => {
  return (
    <div>
      <h1 className="text-3xl uppercase text-slate-700 font-black px-5 mt-5">
        Customers Admin
      </h1>

      {/* TableCustomer component */}

      <TableCustomer />
    </div>
  );
};
