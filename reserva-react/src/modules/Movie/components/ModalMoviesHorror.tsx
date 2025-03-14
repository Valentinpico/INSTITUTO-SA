import { showToast } from "@/adapters/toast/handleToast";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { useBookingContext } from "@/Context/Bookings/BookingProvider";
import { getHorrorMovies_api } from "@/modules/Booking/api/booking.service";
import { zodResolver } from "@hookform/resolvers/zod";
import { Controller, useForm } from "react-hook-form";
import { z } from "zod";

const MoviesHorrorQuerySchema = z.object({
  startDate: z.string().nonempty("La fecha de inicio es requerida"),
  endDate: z.string().nonempty("La fecha de fin es requerida"),
});

export type MoviesHorrorQuery = z.infer<typeof MoviesHorrorQuerySchema>;

export const ModalMoviesHorror = () => {
  const {
    control,
    handleSubmit,
    formState: { errors },
  } = useForm<MoviesHorrorQuery>({
    resolver: zodResolver(MoviesHorrorQuerySchema),
    defaultValues: {
      startDate: new Date().toISOString(),
      endDate: new Date().toISOString(),
    },
  });

  const {setAllBookings}  = useBookingContext ();

  const onSubmit = async (data: MoviesHorrorQuery) => {
    const res = await getHorrorMovies_api(data.startDate, data.endDate);

    showToast(
      res.message || "Horror movies",
      res.success ? "success" : "error"
    );

    setAllBookings(res.data || []);


  };

  const onError = () => {
    showToast("Error the form", "error");
  };

  return (
    <form onSubmit={handleSubmit(onSubmit, onError)} className="space-y-4">
      <div className="space-y-2">
        <label htmlFor="date" className="block text-sm font-medium">
          Fecha de inicio
        </label>

        <div className="p4 bg-white rounded-lg shadow">
          <Controller
            name="startDate"
            control={control}
            render={({ field }) => (
              <Input
                type="date"
                {...field}
                id="startDate"
                value={field.value.split("T")[0] || ""}
                onChange={(e) =>
                  field.onChange(new Date(e.target.value).toISOString())
                }
              />
            )}
          />
        </div>
        {errors.startDate && (
          <p className="text-sm text-red-500">{errors.startDate.message}</p>
        )}
      </div>
      <div className="space-y-2">
        <label htmlFor="date" className="block text-sm font-medium">
          Fecha de fin
        </label>

        <div className="p4 bg-white rounded-lg shadow">
          <Controller
            name="endDate"
            control={control}
            render={({ field }) => (
              <Input
                type="date"
                {...field}
                id="endDate"
                value={field.value.split("T")[0] || ""}
                onChange={(e) =>
                  field.onChange(new Date(e.target.value).toISOString())
                }
              />
            )}
          />
        </div>
        {errors.endDate && (
          <p className="text-sm text-red-500">{errors.endDate.message}</p>
        )}
      </div>

      <Button type="submit" className="w-full">
        Get horror movies
      </Button>
    </form>
  );
};
