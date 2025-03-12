type BadgeStatusProps = {
  status: boolean;
};

export const BadgeStatus = ({ status }: BadgeStatusProps) => {
  return (
    <span
      className={`badge  ${
        status ? "bg-green-600 border-1 border-green-700" : "bg-red-500"
      } text-white p-1 rounded font-bold`}
    >
      {status ? "Activo" : "No Activo"}
    </span>
  );
};
