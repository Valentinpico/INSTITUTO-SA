export const InputError = ({ msg }: { msg: string | undefined }) => {
  return <p className="text-red-500 text-sm">{msg}</p>;
};
