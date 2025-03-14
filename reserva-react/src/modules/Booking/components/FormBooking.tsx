// src/components/SimpleForm.tsx
import { useForm, Controller } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { Button } from "@/components/ui/button";
import {
  Booking,
  BookingCreate,
  initialValuesBooking,
  BookingCreateSchema,
} from "../schemas/BookingSchema";
import { InputError } from "@/components/common/InputError";
import { showToast } from "@/adapters/toast/handleToast";
import { createBooking_api, updateBooking_api } from "../api/booking.service";
import { useEntityContext } from "@/Context/Entities/EntityProvider";
import { useEffect, useState } from "react";

import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import {
  optionsToSelect,
  optionsToSelectBillboards,
  optionsToSelectSeats,
} from "@/modules/Seat/utils/optionsSelects";

import { useBookingContext } from "@/Context/Bookings/BookingProvider";
import { formatDate } from "@/modules/Billboard/components/BillboardCard";
import { Seat } from "@/modules/Seat/schemas/SeatSchema";

export const FormBooking = () => {
  const { setModal, bookingSelected } = useEntityContext();

  const {
    getAllBookings,
    getAllBillboards,
    getAllCustomers,
    allCustomers,
    allBillboards,
  } = useBookingContext();

  const {
    control,
    handleSubmit,
    formState: { errors },
    setValue,
  } = useForm<BookingCreate | Booking>({
    resolver: zodResolver(BookingCreateSchema),
    defaultValues: {
      ...initialValuesBooking,
      billboardID: bookingSelected?.billboardID || 0,
      customerID: bookingSelected?.customerID || 0,
      seatID: bookingSelected?.seatID || 0,
    },
  });

  const optionsCustomerToSelect = optionsToSelect(allCustomers || []);
  const optionsBillboardsToSelect = optionsToSelectBillboards(
    allBillboards || []
  );
  const [seatsByBillboard, setSeatsByBillboard] = useState<Seat[] | null>(null);

  const optionsSeatsToSelect = optionsToSelectSeats(seatsByBillboard || []);

  const onSubmit = async (data: BookingCreate | Booking) => {
    const res = bookingSelected
      ? await updateBooking_api({ id: bookingSelected.id, ...data })
      : await createBooking_api(data);

    showToast(
      res.message || "Booking created successfully",
      res.success ? "success" : "error"
    );

    if (res.success) {
      getAllBookings();
      setModal(false);
      control._reset();
    }
  };
  const onError = () => {
    showToast("There are errors in the form", "error");
    console.log(errors);
  };
  useEffect(() => {
    if (bookingSelected) {
      setValue("date", bookingSelected.date);
      setValue("billboardID", bookingSelected.billboardID);
      setValue("customerID", bookingSelected.customerID);
      setValue("seatID", bookingSelected.seatID);

      const billboard = allBillboards.find(
        (billboard) => billboard.id === Number(bookingSelected.billboardID)
      ) || { room: { seats: [] } };

      const seatsActive = billboard.room?.seats.filter((seat) => seat.status);
      setSeatsByBillboard(seatsActive || null);
    }
  }, [bookingSelected, setValue]);

  useEffect(() => {
    getAllBillboards();
    getAllCustomers();
  }, []);

  return (
    <>
      <h1 className="text-2xl font-bold">
        {bookingSelected ? "Edit Booking" : "Create Booking"}
      </h1>
      <form onSubmit={handleSubmit(onSubmit, onError)} className="space-y-4">
        <div className="space-y-2">
          <label htmlFor="date" className="block text-sm font-medium">
            Fecha
          </label>
          <div className="p4 ">{formatDate(new Date().toISOString())}</div>
        </div>

        <div>
          <label
            htmlFor="customerID"
            className="block text-sm font-medium text-gray-700"
          >
            Customer
          </label>
          <Controller
            name="customerID"
            control={control}
            render={({ field }) => (
              <Select
                onValueChange={(e) => field.onChange(Number(e))}
                value={field.value.toString()}
              >
                <SelectTrigger className="mt-1">
                  <SelectValue placeholder="Select a room" />
                </SelectTrigger>
                <SelectContent>
                  <SelectItem value="0">Select a customer</SelectItem>
                  {optionsCustomerToSelect.map((option) => (
                    <SelectItem
                      key={option.value}
                      value={option.value.toString()}
                    >
                      {option.label}
                    </SelectItem>
                  ))}
                </SelectContent>
              </Select>
            )}
          />
          {errors.customerID && <InputError msg={errors.customerID?.message} />}
        </div>
        <div>
          <label
            htmlFor="billboardID"
            className="block text-sm font-medium text-gray-700"
          >
            Movie
          </label>
          <Controller
            name="billboardID"
            control={control}
            render={({ field }) => (
              <Select
                onValueChange={(e) => {
                  field.onChange(Number(e));
                  const billboard = allBillboards.find(
                    (billboard) => billboard.id === Number(e)
                  ) || { room: { seats: [] } };
                  const seatsActive = billboard.room?.seats.filter((seat) => seat.status);
                  setSeatsByBillboard(seatsActive || null);
                }}
                value={field.value.toString()}
              >
                <SelectTrigger className="mt-1">
                  <SelectValue placeholder="Select a room" />
                </SelectTrigger>
                <SelectContent>
                  <SelectItem value="0">Select a movie</SelectItem>
                  {optionsBillboardsToSelect.map((option) => (
                    <SelectItem
                      key={option.value}
                      value={option.value.toString()}
                    >
                      {option.label}
                    </SelectItem>
                  ))}
                </SelectContent>
              </Select>
            )}
          />
          {errors.billboardID && (
            <InputError msg={errors.billboardID?.message} />
          )}
        </div>
        {seatsByBillboard && seatsByBillboard.length > 0 && (
          <div>
            <label
              htmlFor="seatID"
              className="block text-sm font-medium text-gray-700"
            >
              Seat
            </label>
            <Controller
              name="seatID"
              control={control}
              render={({ field }) => (
                <Select
                  onValueChange={(e) => field.onChange(Number(e))}
                  value={field.value.toString()}
                >
                  <SelectTrigger className="mt-1">
                    <SelectValue placeholder="Select a seat" />
                  </SelectTrigger>
                  <SelectContent>
                    <SelectItem value="0">Select a seat</SelectItem>
                    {optionsSeatsToSelect.map((option) => (
                      <SelectItem
                        key={option.value}
                        value={option.value.toString()}
                      >
                        {option.label}
                      </SelectItem>
                    ))}
                  </SelectContent>
                </Select>
              )}
            />
            {errors.seatID && <InputError msg={errors.seatID?.message} />}
          </div>
        )}

        <Button type="submit" className="w-full">
          {bookingSelected ? "Update Booking" : "Create Booking"}
        </Button>
      </form>{" "}
    </>
  );
};
