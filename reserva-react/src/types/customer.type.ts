export type Customer = {
  id: string;
  status: boolean;
  documentNumber: string;
  name: string;
  lastName: string;
  age: number;
  phoneNumber: string;
  email: string;
};
export type CustomerCreate = Omit<Customer, "id" | "status">;
