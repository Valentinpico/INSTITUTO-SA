import { ApiResponseSchema } from "@/schemas/ApiResponseSchema";
import { z } from "zod";

export const initialValuesCustomer: Customer = {
  id: 0,
  documentNumber: "",
  name: "",
  lastname: "",
  age: 0,
  phoneNumber: "",
  email: "",
  status: true,
};

export const UserSchema = z.object({
  id: z.number(),
  documentNumber: z.string().min(1, "The document number is required"),
  name: z.string().min(1, "The name is required"),
  lastname: z.string().min(1, "The lastname is required"),
  age: z.number().min(1, "The age is required"),
  phoneNumber: z.string().min(1, "The phone number is required"),
  email: z.string().email("The email is invalid"),
  status: z.boolean(),
});
export type Customer = z.infer<typeof UserSchema>;

export const UserCreateSchema = UserSchema.omit({ id: true });
export type CustomerCreate = z.infer<typeof UserCreateSchema>;

export const UserApiSchema = ApiResponseSchema(UserSchema);
export const UsersApiSchema = ApiResponseSchema(z.array(UserSchema));
