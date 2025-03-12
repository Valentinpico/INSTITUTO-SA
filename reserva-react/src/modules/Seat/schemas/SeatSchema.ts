import { RoomSchema } from "@/modules/Room/schemas/RoomSchema";
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
  roomID: RoomSchema.shape.id,
});

export type Seat = z.infer<typeof SeatSchema>;

export const SeatCreateSchema = SeatSchema.omit({ id: true });
export type SeatCreate = z.infer<typeof SeatCreateSchema>;

export const SeatApiSchema = ApiResponseSchema(SeatSchema);
export const SeatsApiSchema = ApiResponseSchema(z.array(SeatSchema));
