import { MovieSchema } from "@/modules/Movie/schemas/MovieSchema";
import { RoomSchema } from "@/modules/Room/schemas/RoomSchema";
import { ApiResponseSchema } from "@/schemas/ApiResponseSchema";
import { z } from "zod";

export const initialValuesBillboard: Billboard = {
  id: 0,
  status: true,
  date: new Date().toISOString(),
  startTime: "",
  endTime: "",
  movieID: 0,
  roomID: 0,
};

export const BillboardSchema = z.object({
  id: z.number().min(1, "The id is required"),
  status: z.boolean(),
  date: z.string(),
  startTime: z.string().regex(/^([01]?\d|2[0-3]):([0-5]?\d):([0-5]?\d)$/, {
    message: "El formato debe ser hh:mm:ss",
  }),
  endTime: z.string().regex(/^([01]?\d|2[0-3]):([0-5]?\d):([0-5]?\d)$/, {
    message: "El formato debe ser hh:mm:ss",
  }),
  movieID: MovieSchema.shape.id,
  roomID: RoomSchema.shape.id,
});
export type Billboard = z.infer<typeof BillboardSchema>;

export const BillboardCreateSchema = BillboardSchema.omit({ id: true });
export type BillboardCreate = z.infer<typeof BillboardCreateSchema>;

export const BillboardApiSchema = ApiResponseSchema(BillboardSchema);
export const BillboardsApiSchema = ApiResponseSchema(z.array(BillboardSchema));
