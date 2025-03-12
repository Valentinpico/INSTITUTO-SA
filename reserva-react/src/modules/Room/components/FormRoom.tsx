// src/components/SimpleForm.tsx
import { useForm, Controller } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import {
  RoomCreate,
  Room,
  initialValuesRoom,
  RoomCreateSchema,
} from "../schemas/RoomSchema";
import { InputError } from "@/components/common/InputError";
import { showToast } from "@/adapters/toast/handleToast";
import { createRoom_api, updateRoom_api } from "../api/room.service";
import { useBookingContext } from "@/Context/BookingProvider";
import { useEffect } from "react";

type FormCustomerProps = {
  getAllRooms: () => void;
};

export const FormRoom = ({ getAllRooms }: FormCustomerProps) => {
  const {
    control,
    handleSubmit,
    formState: { errors },
    setValue,
  } = useForm<RoomCreate | Room>({
    resolver: zodResolver(RoomCreateSchema),
    defaultValues: initialValuesRoom,
  });

  const { setModal, roomSelected } = useBookingContext();

  const onSubmit = async (data: RoomCreate | Room) => {
    const res = roomSelected
      ? await updateRoom_api({ id: roomSelected.id, ...data })
      : await createRoom_api(data);

    showToast(
      res.message || "Customer created successfully",
      res.success ? "success" : "error"
    );

    if (res.success) {
      getAllRooms();
      setModal(false);
      control._reset();
    }
  };

  const onError = () => {
    showToast("There are errors in the form", "error");
  };

  useEffect(() => {
    if (roomSelected) {
      setValue("name", roomSelected.name);
      setValue("number", roomSelected.number);
    }
  }, [roomSelected, setValue]);
  return (
    <>
      <h1 className="text-2xl font-bold">
        {roomSelected ? "Edit Customer" : "Create Customer"}
      </h1>
      <form onSubmit={handleSubmit(onSubmit, onError)} className="space-y-4">
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
          <label
            htmlFor="documentNumber"
            className="block text-sm font-medium text-gray-700"
          >
            Number
          </label>
          <Controller
            name="number"
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
          {errors.number && <InputError msg={errors.number?.message} />}
        </div>

        <Button type="submit" className="w-full">
          {roomSelected ? "Update Room" : "Create Room"}
        </Button>
      </form>{" "}
    </>
  );
};
