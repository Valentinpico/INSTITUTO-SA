import { API_URL } from "@/utils/env";
import axios from "axios";
import {
  Customer,
  CustomerCreate,
  UserApiSchema,
  UsersApiSchema,
} from "../schemas/CustomerSchema";
import { ApiResponse } from "@/schemas/ApiResponseSchema";

const uri = `${API_URL}/customer`;

export const getCustomers_api = async () => {
  try {
    const response = await axios.get(uri);

    const validateResponse = UsersApiSchema.safeParse(response.data);

    if (!validateResponse.success) {
      throw new Error(validateResponse.error.message);
    }

    return validateResponse.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error.response?.data as ApiResponse<Customer[]>;
    }
    throw error;
  }
};

export const getCustomerById_api = async (id: Customer["id"]) => {
  try {
    const response = await axios.get(`${uri}/${id}`);

    const validateResponse = UserApiSchema.safeParse(response.data);

    if (!validateResponse.success) {
      console.log(validateResponse.error.errors);
      throw new Error(validateResponse.error.message);
    }

    return validateResponse.data;
  } catch (error) {
    console.error(`Error fetching customer with id ${id}:`, error);
    throw error;
  }
};

export const createCustomer_api = async (customerCreate: CustomerCreate) => {
  try {
    const response = await axios.post(uri, customerCreate);

    const validateResponse = UserApiSchema.safeParse(response.data);

    if (!validateResponse.success) {
      throw new Error(validateResponse.error.message);
    }

    return validateResponse.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error.response?.data as ApiResponse<Customer>;
    }
    throw error;
  }
};

export const updateCustomer_api = async (customer: Customer) => {
  try {
    const response = await axios.put(`${uri}/`, customer);

    const validateResponse = UserApiSchema.safeParse(response.data);

    if (!validateResponse.success) {
      console.log(validateResponse.error.errors);
      throw new Error(validateResponse.error.message);
    }

    return validateResponse.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error.response?.data as ApiResponse<Customer>;
    }
    throw error;
  }
};

export const deleteCustomer_api = async (id: Customer["id"]) => {
  try {
    const response = await axios.delete(`${uri}/${id}`);

    const validateResponse = UserApiSchema.safeParse(response.data);

    if (!validateResponse.success) {
      console.log(validateResponse.error.errors);
      throw new Error(validateResponse.error.message);
    }

    return validateResponse.data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      return error.response?.data as ApiResponse<Customer>;
    }
    throw error;
  }
};
