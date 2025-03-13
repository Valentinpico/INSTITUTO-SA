// src/components/SimpleForm.tsx
import { useForm, Controller } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import {
  SeatCreate,
  Seat,
  initialValuesSeat,
  SeatCreateSchema,
} from "../schemas/SeatSchema";
import { InputError } from "@/components/common/InputError";
import { showToast } from "@/adapters/toast/handleToast";
import { createSeat_api, updateSeat_api } from "../api/seat.service";
import { useEntityContext } from "@/Context/Entities/EntityProvider";
import { useEffect } from "react";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { useBookingContext } from "@/Context/Bookings/BookingProvider";
import { optionsToSelect } from "../utils/optionsSelects";

export const FormSeat = () => {
  const { setModal, seatSelected } = useEntityContext();
  const {
    control,
    handleSubmit,
    formState: { errors },
    setValue,
  } = useForm<SeatCreate | Seat>({
    resolver: zodResolver(SeatCreateSchema),
    defaultValues: { ...initialValuesSeat, roomID: seatSelected?.roomID || 0 },
  });

  const { getAllSeats, allRooms } = useBookingContext();

  const onSubmit = async (data: SeatCreate | Seat) => {
    const res = seatSelected
      ? await updateSeat_api({ id: seatSelected.id, ...data })
      : await createSeat_api(data);

    showToast(
      res.message || "Customer created successfully",
      res.success ? "success" : "error"
    );

    if (res.success) {
      getAllSeats();
      setModal(false);
      control._reset();
    }
  };

  const onError = () => {
    showToast("There are errors in the form", "error");
  };

  const rooms = optionsToSelect(allRooms);

  useEffect(() => {
    if (!seatSelected) return;

    setValue("rowNumber", seatSelected.rowNumber);
    setValue("number", seatSelected.number);
    setValue("roomID", seatSelected.roomID);
  }, [seatSelected, setValue]);

  return (
    <>
      <h1 className="text-2xl font-bold">
        {seatSelected ? "Edit room" : "Create room"}
      </h1>
      <form onSubmit={handleSubmit(onSubmit, onError)} className="space-y-4">
        <div>
          <label
            htmlFor="number"
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
                id="number"
                placeholder="Enter your number"
                className="mt-1"
                type="number"
                onChange={(e) => field.onChange(Number(e.target.value))}
              />
            )}
          />
          {errors.number && <InputError msg={errors.number?.message} />}
        </div>
        <div>
          <label
            htmlFor="rowNumber"
            className="block text-sm font-medium text-gray-700"
          >
            Row Number
          </label>
          <Controller
            name="rowNumber"
            control={control}
            render={({ field }) => (
              <Input
                {...field}
                id="rowNumber"
                placeholder="Enter your rowNumber"
                className="mt-1"
                type="number"
                onChange={(e) => field.onChange(Number(e.target.value))}
              />
            )}
          />
          {errors.rowNumber && <InputError msg={errors.rowNumber?.message} />}
        </div>
        <div>
          <label
            htmlFor="category"
            className="block text-sm font-medium text-gray-700"
          >
            Room
          </label>
          <Controller
            name="roomID"
            control={control}
            render={({ field }) => {
              console.log(field.value);
              return (
                <Select
                  onValueChange={(e) => field.onChange(Number(e))}
                  value={field.value.toString()}
                >
                  <SelectTrigger className="mt-1">
                    <SelectValue placeholder="Select a room" />
                  </SelectTrigger>
                  <SelectContent>
                    <SelectItem value="0">Select a room</SelectItem>
                    {rooms.map((option) => (
                      <SelectItem
                        key={option.value}
                        value={option.value.toString()}
                      >
                        {option.label}
                      </SelectItem>
                    ))}
                  </SelectContent>
                </Select>
              );
            }}
          />
          {errors.roomID && <InputError msg={errors.roomID?.message} />}
        </div>

        <Button type="submit" className="w-full">
          {seatSelected ? "Update Seat" : "Create Seat"}
        </Button>
      </form>{" "}
    </>
  );
};
