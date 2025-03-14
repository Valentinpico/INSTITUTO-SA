import { BillboardSchema } from "@/modules/Billboard/schemas/BillboardSchema";
import { UserSchema } from "@/modules/Customer/schemas/CustomerSchema";
import { SeatSchema } from "@/modules/Seat/schemas/SeatSchema";
import { ApiResponseSchema } from "@/schemas/ApiResponseSchema";
import { z } from "zod";

export const initialValuesBooking: Booking = {
  id: 0,
  status: true,
  date: new Date().toISOString(),
  customerID: 0,
  billboardID: 0,
  seatID: 0,
  billboard: null,
  customer: null,
  seat: null,
};

export const BookingSchema = z.object({
  id: z.number().min(1, "The id is required"),
  status: z.boolean(),
  date: z.string(),
  customerID: UserSchema.shape.id,
  billboardID: BillboardSchema.shape.id,
  seatID: SeatSchema.shape.id,
  billboard: BillboardSchema.nullable(),
  customer: UserSchema.nullable(),
  seat: SeatSchema.nullable(),
});

export type Booking = z.infer<typeof BookingSchema>;

export const BookingCreateSchema = BookingSchema.omit({ id: true });
export type BookingCreate = z.infer<typeof BookingCreateSchema>;

export const BookingApiSchema = ApiResponseSchema(BookingSchema);
export const BookingsApiSchema = ApiResponseSchema(z.array(BookingSchema));
