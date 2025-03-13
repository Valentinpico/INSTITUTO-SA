// src/components/SimpleForm.tsx
import { useForm, Controller } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import {
  Customer,
  CustomerCreate,
  initialValuesCustomer,
  UserCreateSchema,
} from "../schemas/CustomerSchema";
import { InputError } from "@/components/common/InputError";
import { showToast } from "@/adapters/toast/handleToast";
import {
  createCustomer_api,
  updateCustomer_api,
} from "../api/customer.service";
import { useEntityContext } from "@/Context/Entities/EntityProvider";
import { useEffect } from "react";
import { useBookingContext } from "@/Context/Bookings/BookingProvider";


export const FormCustomer = () => {
  const {
    control,
    handleSubmit,
    formState: { errors },
    setValue,
  } = useForm<CustomerCreate | Customer>({
    resolver: zodResolver(UserCreateSchema),
    defaultValues: initialValuesCustomer,
  });

  const { setModal, customerSelected } = useEntityContext();

  const { getAllCustomers } = useBookingContext();

  const onSubmit = async (data: CustomerCreate | Customer) => {
    const res = customerSelected
      ? await updateCustomer_api({ id: customerSelected.id, ...data })
      : await createCustomer_api(data);

    showToast(
      res.message || "Customer created successfully",
      res.success ? "success" : "error"
    );

    if (res.success) {
      getAllCustomers();
      setModal(false);
      control._reset();
    }
  };

  const onError = () => {
    showToast("There are errors in the form", "error");
  };

  useEffect(() => {
    if (customerSelected) {
      setValue("documentNumber", customerSelected.documentNumber);
      setValue("name", customerSelected.name);
      setValue("lastname", customerSelected.lastname);
      setValue("age", customerSelected.age);
      setValue("phoneNumber", customerSelected.phoneNumber);
      setValue("email", customerSelected.email);
    }
  }, [customerSelected, setValue]);
  return (
    <>
      <h1 className="text-2xl font-bold">
        {customerSelected ? "Edit Customer" : "Create Customer"}
      </h1>
      <form onSubmit={handleSubmit(onSubmit, onError)} className="space-y-4">
        <div>
          <label
            htmlFor="documentNumber"
            className="block text-sm font-medium text-gray-700"
          >
            Document Number
          </label>
          <Controller
            name="documentNumber"
            control={control}
            render={({ field }) => (
              <Input
                {...field}
                id="documentNumber"
                placeholder="Enter your document number"
                className="mt-1"
              />
            )}
          />
          {errors.documentNumber && (
            <InputError msg={errors.documentNumber?.message} />
          )}
        </div>

        <div>
          <label
            htmlFor="name"
            className="block text-sm font-medium text-gray-700"
          >
            Name
          </label>
          <Controller
            name="name"
            control={control}
            render={({ field }) => (
              <Input
                {...field}
                id="name"
                placeholder="Enter your name"
                className="mt-1"
              />
            )}
          />
          {errors.name && <InputError msg={errors.name?.message} />}
        </div>

        <div>
          <label htmlFor="" className="block text-sm font-medium text-gray-700">
            Lastname
          </label>
          <Controller
            name="lastname"
            control={control}
            render={({ field }) => (
              <Input
                {...field}
                id="lastname"
                placeholder="Enter your lastname"
                className="mt-1"
              />
            )}
          />
          {errors.lastname && <InputError msg={errors.lastname?.message} />}
        </div>
        <div>
          <label
            htmlFor="documentNumber"
            className="block text-sm font-medium text-gray-700"
          >
            Age
          </label>
          <Controller
            name="age"
            control={control}
            render={({ field }) => (
              <Input
                {...field}
                id="age"
                type="number"
                placeholder="0"
                className="mt-1"
                onChange={(e) => field.onChange(Number(e.target.value))}
              />
            )}
          />
          {errors.age && <InputError msg={errors.age?.message} />}
        </div>
        <div>
          <label
            htmlFor="phoneNumber"
            className="block text-sm font-medium text-gray-700"
          >
            Phone Number
          </label>
          <Controller
            name="phoneNumber"
            control={control}
            render={({ field }) => (
              <Input
                {...field}
                id="phoneNumber"
                placeholder="Enter your phone number"
                className="mt-1"
              />
            )}
          />
          {errors.phoneNumber && (
            <InputError msg={errors.phoneNumber?.message} />
          )}
        </div>
        <div>
          <label
            htmlFor="email"
            className="block text-sm font-medium text-gray-700"
          >
            Email
          </label>
          <Controller
            name="email"
            control={control}
            render={({ field }) => (
              <Input
                {...field}
                id="email"
                placeholder="Enter your email"
                className="mt-1"
              />
            )}
          />
          {errors.email && <InputError msg={errors.email?.message} />}
        </div>

        {/* Select */}
        {/*      <div>
        <label
          htmlFor="category"
          className="block text-sm font-medium text-gray-700"
        >
          Categoría
        </label>
        <Controller
          name="category"
          control={control}
          render={({ field }) => (
            <Select onValueChange={field.onChange} value={field.value}>
              <SelectTrigger className="mt-1">
                <SelectValue placeholder="Selecciona una categoría" />
              </SelectTrigger>
              <SelectContent>
                <SelectItem value="cat1">Categoría 1</SelectItem>
                <SelectItem value="cat2">Categoría 2</SelectItem>
                <SelectItem value="cat3">Categoría 3</SelectItem>
              </SelectContent>
            </Select>
          )}
        />
        {errors.category && (
          <p className={errorClass}>{errors.category.message}</p>
        )}
      </div> */}

        {/* Campo de fecha */}
        {/*  <div>
        <label
          htmlFor="date"
          className="block text-sm font-medium text-gray-700"
        >
          Fecha
        </label>
        <Controller
          name="date"
          control={control}
          render={({ field }) => (
            <Calendar
              mode="single"
              selected={field.value}
              onSelect={field.onChange}
              className="mt-1 rounded-md border"
            />
          )}
        />
        {errors.date && (
          <p className={errorClass}>{errors.date.message}</p>
        )}
      </div> */}

        {/* Botón de enviar */}
        <Button type="submit" className="w-full">
          {customerSelected ? "Update Customer" : "Create Customer"}
        </Button>
      </form>{" "}
    </>
  );
};
