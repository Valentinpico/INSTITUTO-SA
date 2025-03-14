import { ApiResponseSchema } from "@/schemas/ApiResponseSchema";
import { z } from "zod";

export const initialValuesSeat: Seat = {
  id: 0,
  status: true,
  number: 0,
  rowNumber: 0,
  roomID: 0,
};

export const SeatSchema = z.object({
  id: z.number().min(1, "The id is required"),
  status: z.boolean(),
  number: z.number().min(1, "The number is required"),
  rowNumber: z.number().min(1, "The row number is required"),
  roomID: z.number().min(1, "The room ID is required"),
});

export const SeatsAvailableOrOccupiedSchema = z.object({
  available: z.number().min(0, "The available is required"),
  occupied: z.number().min(0, "The occupied is required"),
  total: z.number().min(0, "The total is required"),
  roomID: z.number().min(1, "The room ID is required"),
  room: z.object({
    id: z.number().min(1, "The id is required"),
    status: z.boolean(),
    name: z.string().min(1, "The name is required"),
    number: z.number().min(1, "The number is required"),
    seats: z.array(SeatSchema),
  }),
});

export type SeatsAvailableOrOccupied = z.infer<
  typeof SeatsAvailableOrOccupiedSchema
>;
export type Seat = z.infer<typeof SeatSchema>;

export const SeatCreateSchema = SeatSchema.omit({ id: true });
export type SeatCreate = z.infer<typeof SeatCreateSchema>;

export const SeatApiSchema = ApiResponseSchema(SeatSchema);
export const SeatsApiSchema = ApiResponseSchema(z.array(SeatSchema));
export const SeatsAvailableOrOccupiedApiSchema = ApiResponseSchema(
  z.array(SeatsAvailableOrOccupiedSchema)
);
