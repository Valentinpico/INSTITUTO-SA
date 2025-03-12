import { API_URL } from "@/utils/env";
import axios from "axios";
import {
  Movie,
  MovieCreate,
  MovieApiSchema,
  MoviesApiSchema,
} from "../schemas/MovieSchema";
import { ApiResponse } from "@/schemas/ApiResponseSchema";

const uri = `${API_URL}/movie`;
export const getMovies_api = async () => {
  try {
    const response = await axios.get(uri);

    const validateResponse = MoviesApiSchema.safeParse(response.data);

    if (!validateResponse.success) {
      throw new Error(validateResponse.error.message);
    }

    return validateResponse.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error.response?.data as ApiResponse<Movie[]>;
    }
    throw error;
  }
};

export const getMovieById_api = async (id: Movie["id"]) => {
  try {
    const response = await axios.get(`${uri}/${id}`);

    const validateResponse = MovieApiSchema.safeParse(response.data);

    if (!validateResponse.success) {
      console.log(validateResponse.error.errors);
      throw new Error(validateResponse.error.message);
    }

    return validateResponse.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error.response?.data as ApiResponse<Movie>;
    }
    throw error;
  }
};

export const createMovie_api = async (roomCreate: MovieCreate) => {
  try {
    const response = await axios.post(uri, roomCreate);

    const validateResponse = MovieApiSchema.safeParse(response.data);

    if (!validateResponse.success) {
      throw new Error(validateResponse.error.message);
    }

    return validateResponse.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error.response?.data as ApiResponse<Movie>;
    }
    throw error;
  }
};

export const updateMovie_api = async (Movie: Movie) => {
  try {
    const response = await axios.put(`${uri}/`, Movie);

    const validateResponse = MovieApiSchema.safeParse(response.data);

    if (!validateResponse.success) {
      console.log(validateResponse.error.errors);
      throw new Error(validateResponse.error.message);
    }

    return validateResponse.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error.response?.data as ApiResponse<Movie>;
    }
    throw error;
  }
};

export const deleteMovie_api = async (id: Movie["id"]) => {
  try {
    const response = await axios.delete(`${uri}/${id}`);

    const validateResponse = MovieApiSchema.safeParse(response.data);

    if (!validateResponse.success) {
      console.log(validateResponse.error.errors);
      throw new Error(validateResponse.error.message);
    }

    return validateResponse.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error.response?.data as ApiResponse<Movie>;
    }
    throw error;
  }
};
