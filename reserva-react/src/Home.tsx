import { Column, Row, TablaDinamica } from "./Components/TablaDinamica";

export const Home = () => {
  const columns: Column[] = [
    { key: "name", header: "Name" },
    { key: "age", header: "Age" },
    { key: "action", header: "Action" },
  ];

  const rows: Row[] = [
    { name: "John Doe", age: 28, action: "Edit" },
    { name: "Jane Smith", age: 34, action: "Delete" },
    {
      name: "Alice Johnson",
      age: 25,
      render: (row: Row) => (
        <button onClick={() => alert(`Editing ${row.name}`)}>Edit</button>
      ),
    },
  ];
  return (
    <>
      <h1>Home</h1>
      <div>
        <h1>Dynamic Table</h1>
        <TablaDinamica columns={columns} rows={rows} />
      </div>
    </>
  );
};
