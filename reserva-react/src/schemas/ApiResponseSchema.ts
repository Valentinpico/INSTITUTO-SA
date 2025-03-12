import { z } from "zod";

export type ApiResponse<T> = {
  success: boolean;
  data?: T;
  errors?: string[];
  message?: string;
  statusCode: number;
};

export const ApiResponseSchema = <T>(data: z.ZodType<T>) =>
  z.object({
    success: z.boolean(),
    data: data.nullable(),
    errors: z.array(z.string()).optional(),
    message: z.string().optional(),
    statusCode: z.number(),
  }) as z.ZodType<ApiResponse<T>>;
