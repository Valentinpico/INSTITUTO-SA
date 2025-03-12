import { API_URL } from "@/utils/env";
import axios from "axios";
import {
  Seat,
  SeatCreate,
  SeatApiSchema,
  SeatsApiSchema,
} from "../schemas/SeatSchema";
import { ApiResponse } from "@/schemas/ApiResponseSchema";
import { showToast } from "@/adapters/toast/handleToast";

const uri = `${API_URL}/Seat`;

export const getSeats_api = async () => {
  try {
    const response = await axios.get(uri);

    const validateResponse = SeatsApiSchema.safeParse(response.data);

    if (!validateResponse.success) {
      showToast("error con la validacion del schema", "error");
      throw new Error(validateResponse.error.message);
    }

    return validateResponse.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error.response?.data as ApiResponse<Seat[]>;
    }
    throw error;
  }
};

export const getSeatById_api = async (id: Seat["id"]) => {
  try {
    const response = await axios.get(`${uri}/${id}`);

    const validateResponse = SeatApiSchema.safeParse(response.data);

    if (!validateResponse.success) {
      showToast("error con la validacion del schema", "error");
      console.log(validateResponse.error.errors);
      throw new Error(validateResponse.error.message);
    }

    return validateResponse.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error.response?.data as ApiResponse<Seat>;
    }
    throw error;
  }
};

export const createSeat_api = async (roomCreate: SeatCreate) => {
  try {
    const response = await axios.post(uri, roomCreate);

    const validateResponse = SeatApiSchema.safeParse(response.data);

    if (!validateResponse.success) {
      showToast("error con la validacion del schema", "error");
      throw new Error(validateResponse.error.message);
    }

    return validateResponse.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error.response?.data as ApiResponse<Seat>;
    }
    throw error;
  }
};

export const updateSeat_api = async (Seat: Seat) => {
  try {
    const response = await axios.put(`${uri}/`, Seat);

    const validateResponse = SeatApiSchema.safeParse(response.data);

    if (!validateResponse.success) {
      showToast("error con la validacion del schema", "error");
      console.log(validateResponse.error.errors);
      throw new Error(validateResponse.error.message);
    }

    return validateResponse.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error.response?.data as ApiResponse<Seat>;
    }
    throw error;
  }
};

export const deleteSeat_api = async (id: Seat["id"]) => {
  try {
    const response = await axios.delete(`${uri}/${id}`);

    const validateResponse = SeatApiSchema.safeParse(response.data);

    if (!validateResponse.success) {
      showToast("error con la validacion del schema", "error");
      console.log(validateResponse.error.errors);
      throw new Error(validateResponse.error.message);
    }

    return validateResponse.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error.response?.data as ApiResponse<Seat>;
    }
    throw error;
  }
};
