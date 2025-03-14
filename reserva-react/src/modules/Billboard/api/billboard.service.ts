import { API_URL } from "@/utils/env";
import axios from "axios";
import {
  Billboard,
  BillboardCreate,
  BillboardApiSchema,
  BillboardsApiSchema,
} from "../schemas/BillboardSchema";
import { ApiResponse } from "@/schemas/ApiResponseSchema";
import { showToast } from "@/adapters/toast/handleToast";

const uri = `${API_URL}/billboard`;
export const getBillboards_api = async () => {
  try {
    const response = await axios.get(uri);

    const validateResponse = BillboardsApiSchema.safeParse(response.data);

    if (!validateResponse.success) {
      showToast("error en el schema", "error");
      throw new Error(validateResponse.error.message);
    }

    return validateResponse.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error.response?.data as ApiResponse<Billboard[]>;
    }
    throw error;
  }
};

export const getBillboardById_api = async (id: Billboard["id"]) => {
  try {
    const response = await axios.get(`${uri}/${id}`);

    const validateResponse = BillboardApiSchema.safeParse(response.data);

    if (!validateResponse.success) {
      showToast("error en el schema", "error");

      console.log(validateResponse.error.errors);
      throw new Error(validateResponse.error.message);
    }

    return validateResponse.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error.response?.data as ApiResponse<Billboard>;
    }
    throw error;
  }
};

export const createBillboard_api = async (roomCreate: BillboardCreate) => {
  try {
    const response = await axios.post(uri, roomCreate);

    const validateResponse = BillboardApiSchema.safeParse(response.data);

    if (!validateResponse.success) {
      showToast("error en el schema", "error");

      throw new Error(validateResponse.error.message);
    }

    return validateResponse.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error.response?.data as ApiResponse<Billboard>;
    }
    throw error;
  }
};

export const updateBillboard_api = async (Billboard: Billboard) => {
  try {
    const response = await axios.put(`${uri}/`, Billboard);

    const validateResponse = BillboardApiSchema.safeParse(response.data);

    if (!validateResponse.success) {
      showToast("error en el schema", "error");

      console.log(validateResponse.error.errors);
      throw new Error(validateResponse.error.message);
    }

    return validateResponse.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error.response?.data as ApiResponse<Billboard>;
    }
    throw error;
  }
};

export const deleteBillboard_api = async (id: Billboard["id"]) => {
  try {
    const response = await axios.delete(`${uri}/${id}`);

    const validateResponse = BillboardApiSchema.safeParse(response.data);

    if (!validateResponse.success) {
      showToast("error en el schema", "error");

      console.log(validateResponse.error.errors);
      throw new Error(validateResponse.error.message);
    }

    return validateResponse.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error.response?.data as ApiResponse<Billboard>;
    }
    throw error;
  }
};

export const cancelBillboard_api = async (id: Billboard["id"]) => {
  try {
    const response = await axios.post(`${uri}/cancel/${id}`);

    const validateResponse = BillboardApiSchema.safeParse(response.data);

    if (!validateResponse.success) {
      showToast("error en el schema", "error");

      console.log(validateResponse.error.errors);
      throw new Error(validateResponse.error.message);
    }

    return validateResponse.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error.response?.data as ApiResponse<Billboard>;
    }
    throw error;
  }
}
