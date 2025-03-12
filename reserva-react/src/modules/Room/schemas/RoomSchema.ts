import { ApiResponseSchema } from "@/schemas/ApiResponseSchema";
import { z } from "zod";

export const initialValuesRoom: Room = {
  id: 0,
  status: true,
  name: "",
  number: 0,
};

export const RoomSchema = z.object({
  id: z.number().min(1, "The id is required"),
  status: z.boolean(),
  name: z.string().min(1, "The name is required"),
  number: z.number().min(1, "The number is required"),
});
export type Room = z.infer<typeof RoomSchema>;

export const RoomCreateSchema = RoomSchema.omit({ id: true });
export type RoomCreate = z.infer<typeof RoomCreateSchema>;

export const RoomApiSchema = ApiResponseSchema(RoomSchema);
export const RoomsApiSchema = ApiResponseSchema(z.array(RoomSchema));
