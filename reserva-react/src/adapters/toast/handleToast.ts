import { toast } from "react-toastify";

type ToastType = "success" | "error" | "info" | "warn" | "default";

const TOAST = {
  success: (message: string) => toast.success(message),
  error: (message: string) => toast.error(message),
  info: (message: string) => toast.info(message),
  warn: (message: string) => toast.warn(message),
  default: (message: string) => toast(message),
};

export const showToast = (message: string, type: ToastType = "default") => {
  TOAST[type](message);
};
