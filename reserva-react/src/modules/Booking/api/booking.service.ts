import { API_URL } from "@/utils/env";
import axios from "axios";
import {
  Booking,
  BookingCreate,
  BookingApiSchema,
  BookingsApiSchema,
} from "../schemas/BookingSchema";
import { ApiResponse } from "@/schemas/ApiResponseSchema";
import { showToast } from "@/adapters/toast/handleToast";

const uri = `${API_URL}/booking`;
export const getBookings_api = async () => {
  try {
    const response = await axios.get(uri);

    const validateResponse = BookingsApiSchema.safeParse(response.data);

    if (!validateResponse.success) {
      showToast("error en el schema", "error");
      throw new Error(validateResponse.error.message);
    }

    return validateResponse.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error.response?.data as ApiResponse<Booking[]>;
    }
    throw error;
  }
};

export const getBookingById_api = async (id: Booking["id"]) => {
  try {
    const response = await axios.get(`${uri}/${id}`);

    const validateResponse = BookingApiSchema.safeParse(response.data);

    if (!validateResponse.success) {
      showToast("error en el schema", "error");

      console.log(validateResponse.error.errors);
      throw new Error(validateResponse.error.message);
    }

    return validateResponse.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error.response?.data as ApiResponse<Booking>;
    }
    throw error;
  }
};

export const createBooking_api = async (roomCreate: BookingCreate) => {
  try {
    const response = await axios.post(uri, roomCreate);

    const validateResponse = BookingApiSchema.safeParse(response.data);

    if (!validateResponse.success) {
      showToast("error en el schema", "error");

      throw new Error(validateResponse.error.message);
    }

    return validateResponse.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error.response?.data as ApiResponse<Booking>;
    }
    throw error;
  }
};

export const updateBooking_api = async (Booking: Booking) => {
  try {
    const response = await axios.put(`${uri}/`, Booking);

    const validateResponse = BookingApiSchema.safeParse(response.data);

    if (!validateResponse.success) {
      showToast("error en el schema", "error");

      console.log(validateResponse.error.errors);
      throw new Error(validateResponse.error.message);
    }

    return validateResponse.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error.response?.data as ApiResponse<Booking>;
    }
    throw error;
  }
};

export const deleteBooking_api = async (id: Booking["id"]) => {
  try {
    const response = await axios.delete(`${uri}/${id}`);

    const validateResponse = BookingApiSchema.safeParse(response.data);

    if (!validateResponse.success) {
      showToast("error en el schema", "error");

      console.log(validateResponse.error.errors);
      throw new Error(validateResponse.error.message);
    }

    return validateResponse.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error.response?.data as ApiResponse<Booking>;
    }
    throw error;
  }
};

export const getHorrorMovies_api = async (startDate: string, endDate: string) => {
  try {
    const response = await axios.get(`${uri}/horror`, {
      params: {
        startDate,
        endDate,
      },
    });

    const validateResponse = BookingsApiSchema.safeParse(response.data);

    if (!validateResponse.success) {
      throw new Error(validateResponse.error.message);
    }

    return validateResponse.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error.response?.data as ApiResponse<Booking[]>;
    }
    throw error;
  }
}