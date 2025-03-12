import { API_URL } from "@/utils/env";
import axios from "axios";
import {
  Room,
  RoomCreate,
  RoomApiSchema,
  RoomsApiSchema,
} from "../schemas/RoomSchema";
import { ApiResponse } from "@/schemas/ApiResponseSchema";

const uri = `${API_URL}/Room`;

export const getRooms_api = async () => {
  try {
    const response = await axios.get(uri);

    const validateResponse = RoomsApiSchema.safeParse(response.data);

    if (!validateResponse.success) {
      throw new Error(validateResponse.error.message);
    }

    return validateResponse.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error.response?.data as ApiResponse<Room[]>;
    }
    throw error;
  }
};

export const getRoomById_api = async (id: Room["id"]) => {
  try {
    const response = await axios.get(`${uri}/${id}`);

    const validateResponse = RoomApiSchema.safeParse(response.data);

    if (!validateResponse.success) {
      console.log(validateResponse.error.errors);
      throw new Error(validateResponse.error.message);
    }

    return validateResponse.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error.response?.data as ApiResponse<Room>;
    }
    throw error;
  }
};

export const createRoom_api = async (roomCreate: RoomCreate) => {
  try {
    const response = await axios.post(uri, roomCreate);

    const validateResponse = RoomApiSchema.safeParse(response.data);

    if (!validateResponse.success) {
      throw new Error(validateResponse.error.message);
    }

    return validateResponse.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error.response?.data as ApiResponse<Room>;
    }
    throw error;
  }
};

export const updateRoom_api = async (Room: Room) => {
  try {
    const response = await axios.put(`${uri}/`, Room);

    const validateResponse = RoomApiSchema.safeParse(response.data);

    if (!validateResponse.success) {
      console.log(validateResponse.error.errors);
      throw new Error(validateResponse.error.message);
    }

    return validateResponse.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error.response?.data as ApiResponse<Room>;
    }
    throw error;
  }
};

export const deleteRoom_api = async (id: Room["id"]) => {
  try {
    const response = await axios.delete(`${uri}/${id}`);

    const validateResponse = RoomApiSchema.safeParse(response.data);

    if (!validateResponse.success) {
      console.log(validateResponse.error.errors);
      throw new Error(validateResponse.error.message);
    }

    return validateResponse.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error.response?.data as ApiResponse<Room>;
    }
    throw error;
  }
};
