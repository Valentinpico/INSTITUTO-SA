import { ApiResponseSchema } from "@/schemas/ApiResponseSchema";
import { z } from "zod";

export const initialValuesCustomer: Customer = {
  id: 0,
  status: true,
  name: "",
  documentNumber: "",
  lastname: "",
  age: 0,
  phoneNumber: "",
  email: "",
};

export const UserSchema = z.object({
  id: z.number(),
  status: z.boolean(),
  name: z.string().min(1, "The name is required"),
  documentNumber: z.string().min(1, "The document number is required"),
  lastname: z.string().min(1, "The lastname is required"),
  age: z.number().min(1, "The age is required"),
  phoneNumber: z.string().min(1, "The phone number is required"),
  email: z.string().email("The email is invalid"),
});
export type Customer = z.infer<typeof UserSchema>;

export const UserCreateSchema = UserSchema.omit({ id: true });
export type CustomerCreate = z.infer<typeof UserCreateSchema>;

export const UserApiSchema = ApiResponseSchema(UserSchema);
export const UsersApiSchema = ApiResponseSchema(z.array(UserSchema));
