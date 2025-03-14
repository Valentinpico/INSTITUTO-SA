import { TableBillboard } from "./components/TablaBillboard";

export const BillboardPage = () => {
  return (
    <div>
      <h1 className="text-3xl uppercase text-slate-700 font-black px-5 mt-5">
        Billboard Admin
      </h1>

      <TableBillboard />
    </div>
  );
};
