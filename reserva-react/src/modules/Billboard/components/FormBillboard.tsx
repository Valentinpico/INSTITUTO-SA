// src/components/SimpleForm.tsx
import { useForm, Controller } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import {
  Billboard,
  BillboardCreate,
  initialValuesBillboard,
  BillboardCreateSchema,
} from "../schemas/BillboardSchema";
import { InputError } from "@/components/common/InputError";
import { showToast } from "@/adapters/toast/handleToast";
import {
  createBillboard_api,
  updateBillboard_api,
} from "../api/billboard.service";
import { useEntityContext } from "@/Context/Entities/EntityProvider";
import { useEffect } from "react";

import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import {
  optionsToSelect
} from "@/modules/Seat/utils/optionsSelects";

import { useBookingContext } from "@/Context/Bookings/BookingProvider";

export const FormBillboard = () => {
  const { setModal, billboardSelected } = useEntityContext();

  const { getAllBillboards, getAllMovies, getAllRooms, allRooms, allMovies } =
    useBookingContext();

  const {
    control,
    handleSubmit,
    formState: { errors },
    setValue,
  } = useForm<BillboardCreate | Billboard>({
    resolver: zodResolver(BillboardCreateSchema),
    defaultValues: {
      ...initialValuesBillboard,
      roomID: billboardSelected?.roomID || 0,
      movieID: billboardSelected?.movieID || 0,
    },
  });
  const onSubmit = async (data: BillboardCreate | Billboard) => {
    const res = billboardSelected
      ? await updateBillboard_api({ id: billboardSelected.id, ...data })
      : await createBillboard_api(data);

    showToast(
      res.message || "Billboard created successfully",
      res.success ? "success" : "error"
    );

    if (res.success) {
      getAllBillboards();
      setModal(false);
      control._reset();
    }
  };

  const onError = () => {
    showToast("There are errors in the form", "error");
  };

  const optionsRoomsToSelect = optionsToSelect(allRooms || []);
  const optionsMoviesToSelect = optionsToSelect(allMovies || []);

  useEffect(() => {
    if (billboardSelected) {
      setValue("date", billboardSelected.date);
      setValue("startTime", billboardSelected.startTime);
      setValue("endTime", billboardSelected.endTime);
      setValue("roomID", billboardSelected.roomID);
      setValue("movieID", billboardSelected.movieID);
    }
  }, [billboardSelected, setValue]);

  useEffect(() => {
    getAllRooms();
    getAllMovies();
  }, [getAllMovies, getAllRooms]);

  return (
    <>
      <h1 className="text-2xl font-bold">
        {billboardSelected ? "Edit Billboard" : "Create Billboard"}
      </h1>
      <form onSubmit={handleSubmit(onSubmit, onError)} className="space-y-4">
        <div className="space-y-2">
          <label htmlFor="date" className="block text-sm font-medium">
            Fecha
          </label>

          <div className="p4 bg-white rounded-lg shadow">
            <Controller
              name="date"
              control={control}
              render={({ field }) => (
                <Input
                  type="date"
                  {...field}
                  id="date"
                  value={field.value.split("T")[0] || ""}
                  onChange={(e) =>
                    field.onChange(new Date(e.target.value).toISOString())
                  }
                />
              )}
            />
          </div>
          {errors.date && (
            <p className="text-sm text-red-500">{errors.date.message}</p>
          )}
        </div>

        <div>
          <label
            htmlFor="startTime"
            className="block text-sm font-medium text-gray-700"
          >
            Start Time
          </label>
          <Controller
            name="startTime"
            control={control}
            render={({ field }) => (
              <Input
                {...field}
                id="startTime"
                type="text"
                placeholder="0"
                className="mt-1"
                onChange={(e) => field.onChange(e.target.value)}
              />
            )}
          />
          {errors.startTime && <InputError msg={errors.startTime?.message} />}
        </div>

        <div>
          <label
            htmlFor="endTime"
            className="block text-sm font-medium text-gray-700"
          >
            Start Time
          </label>
          <Controller
            name="endTime"
            control={control}
            render={({ field }) => (
              <Input
                {...field}
                id="endTime"
                type="text"
                placeholder="0"
                className="mt-1"
                onChange={(e) => field.onChange(e.target.value)}
              />
            )}
          />
          {errors.endTime && <InputError msg={errors.endTime?.message} />}
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
                    {optionsRoomsToSelect.map((option) => (
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
        <div>
          <label
            htmlFor="category"
            className="block text-sm font-medium text-gray-700"
          >
            Movie
          </label>
          <Controller
            name="movieID"
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
                    {optionsMoviesToSelect.map((option) => (
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
          {errors.movieID && <InputError msg={errors.movieID?.message} />}
        </div>

        <Button type="submit" className="w-full">
          {billboardSelected ? "Update Billboard" : "Create Billboard"}
        </Button>
      </form>{" "}
    </>
  );
};
