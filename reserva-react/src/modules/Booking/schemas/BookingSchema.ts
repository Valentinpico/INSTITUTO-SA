import {
  BillboardSchema,
  initialValuesBillboard,
} from "@/modules/Billboard/schemas/BillboardSchema";
import {
  initialValuesCustomer,
  UserSchema,
} from "@/modules/Customer/schemas/CustomerSchema";
import {
  initialValuesSeat,
  SeatSchema,
} from "@/modules/Seat/schemas/SeatSchema";
import { ApiResponseSchema } from "@/schemas/ApiResponseSchema";
import { z } from "zod";

export const initialValuesBooking: Booking = {
  id: 0,
  status: true,
  date: new Date().toISOString(),
  customerID: 0,
  billboardID: 0,
  seatID: 0,
  billboard: initialValuesBillboard,
  customer: initialValuesCustomer,
  seat: initialValuesSeat,
};

export const BookingSchema = z.object({
  id: z.number().min(1, "The id is required"),
  status: z.boolean(),
  date: z.string(),
  customerID: UserSchema.shape.id,
  billboardID: BillboardSchema.shape.id,
  seatID: SeatSchema.shape.id.nullable(),
  billboard: BillboardSchema,
  customer: UserSchema,
  seat: SeatSchema,
});

export type Booking = z.infer<typeof BookingSchema>;

export const BookingCreateSchema = BookingSchema.omit({ id: true });
export type BookingCreate = z.infer<typeof BookingCreateSchema>;

export const BookingApiSchema = ApiResponseSchema(BookingSchema);
export const BookingsApiSchema = ApiResponseSchema(z.array(BookingSchema));
