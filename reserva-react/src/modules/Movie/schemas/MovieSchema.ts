import { ApiResponseSchema } from "@/schemas/ApiResponseSchema";
import { MovieGenreEnum } from "../utils/enums";
import { z } from "zod";

export const initialValuesMovie: Movie = {
  id: 0,
  status: true,
  name: "",
  genre: MovieGenreEnum.DEFAULT,
  allowedAge: 0,
  lengthMinutes: 0,
};

export const MovieSchema = z.object({
  id: z.number(),
  status: z.boolean(),
  name: z.string().min(1, "The name is required"),
  genre: z
    .number()
    .min(MovieGenreEnum.ACTION, "The genre is required")
    .max(MovieGenreEnum.WESTERN, "The genre is required"),
  allowedAge: z.number().min(1, "The allowed age is required"),
  lengthMinutes: z.number().min(1, "The length minutes is required"),
});

export type Movie = z.infer<typeof MovieSchema>;

export const MovieCreateSchema = MovieSchema.omit({ id: true });
export type MovieCreate = z.infer<typeof MovieCreateSchema>;

export const MovieApiSchema = ApiResponseSchema(MovieSchema);
export const MoviesApiSchema = ApiResponseSchema(z.array(MovieSchema));
