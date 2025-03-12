// src/components/SimpleForm.tsx
import { useForm, Controller } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import {
  Movie,
  MovieCreate,
  initialValuesMovie,
  MovieCreateSchema,
} from "../schemas/MovieSchema";
import { InputError } from "@/components/common/InputError";
import { showToast } from "@/adapters/toast/handleToast";
import { createMovie_api, updateMovie_api } from "../api/movie.service";
import { useBookingContext } from "@/Context/BookingProvider";
import { useEffect } from "react";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { optionsMovieGenre } from "../utils/enums";

type FormMovieProps = {
  getAllMovies: () => void;
};

export const FormMovie = ({ getAllMovies }: FormMovieProps) => {
  const {
    control,
    handleSubmit,
    formState: { errors },
    setValue,
  } = useForm<MovieCreate | Movie>({
    resolver: zodResolver(MovieCreateSchema),
    defaultValues: initialValuesMovie,
  });

  const { setModal, movieSelected } = useBookingContext();

  const onSubmit = async (data: MovieCreate | Movie) => {
    const res = movieSelected
      ? await updateMovie_api({ id: movieSelected.id, ...data })
      : await createMovie_api(data);

    showToast(
      res.message || "Movie created successfully",
      res.success ? "success" : "error"
    );

    if (res.success) {
      getAllMovies();
      setModal(false);
      control._reset();
    }
  };

  const onError = () => {
    showToast("There are errors in the form", "error");
  };

  useEffect(() => {
    if (movieSelected) {
      setValue("name", movieSelected.name);
      setValue("genre", movieSelected.genre);
      setValue("allowedAge", movieSelected.allowedAge);
      setValue("lengthMinutes", movieSelected.lengthMinutes);
    }
  }, [movieSelected, setValue]);
  return (
    <>
      <h1 className="text-2xl font-bold">
        {movieSelected ? "Edit Movie" : "Create Movie"}
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
            htmlFor="category"
            className="block text-sm font-medium text-gray-700"
          >
            Gender
          </label>
          <Controller
            name="genre"
            control={control}
            render={({ field }) => (
              <Select
                onValueChange={(e) => field.onChange(Number(e))}
                value={field.value.toString()}
              >
                <SelectTrigger className="mt-1">
                  <SelectValue placeholder="Selecciona una categoría" />
                </SelectTrigger>
                <SelectContent>
                  <SelectItem value="-1">Select a genre</SelectItem>
                  {optionsMovieGenre.map((option) => (
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
          {errors.genre && <InputError msg={errors.genre?.message} />}
        </div>

        <div>
          <label
            htmlFor="allowedAge"
            className="block text-sm font-medium text-gray-700"
          >
            Allowed Age
          </label>
          <Controller
            name="allowedAge"
            control={control}
            render={({ field }) => (
              <Input
                {...field}
                id="allowedAge"
                type="number"
                placeholder="0"
                className="mt-1"
                onChange={(e) => field.onChange(Number(e.target.value))}
              />
            )}
          />
          {errors.allowedAge && <InputError msg={errors.allowedAge?.message} />}
        </div>

        <div>
          <label
            htmlFor="lengthMinutes"
            className="block text-sm font-medium text-gray-700"
          >
            Length Minutes
          </label>
          <Controller
            name="lengthMinutes"
            control={control}
            render={({ field }) => (
              <Input
                {...field}
                id="lengthMinutes"
                placeholder="Enter your phone number"
                className="mt-1"
                onChange={(e) => field.onChange(Number(e.target.value))}
              />
            )}
          />
          {errors.lengthMinutes && (
            <InputError msg={errors.lengthMinutes?.message} />
          )}
        </div>

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
          {movieSelected ? "Update Movie" : "Create Movie"}
        </Button>
      </form>{" "}
    </>
  );
};
