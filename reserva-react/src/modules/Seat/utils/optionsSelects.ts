import {
  formatDate,
  formatTime,
} from "@/modules/Billboard/components/BillboardCard";
import { Billboard } from "@/modules/Billboard/schemas/BillboardSchema";
import { Customer } from "@/modules/Customer/schemas/CustomerSchema";
import { Movie } from "@/modules/Movie/schemas/MovieSchema";
import { Room } from "@/modules/Room/schemas/RoomSchema";
import { Seat } from "@/modules/Seat/schemas/SeatSchema";

export interface OptionsSelect {
  value: number;
  label: string;
}

export const optionsToSelect = (
  rooms: Room[] | Movie[] | Customer[]
): OptionsSelect[] => {
  if (rooms.length === 0) return [];

  return rooms.map((room) => ({
    value: room.id,
    label: room.name,
  }));
};

export const optionsToSelectSeats = (seats: Seat[]): OptionsSelect[] => {
  if (seats.length === 0) return [];

  return seats.map((room) => ({
    value: room.id,
    label: `Row ${room.rowNumber} - Number ${room.number}`,
  }));
};

export const optionsToSelectBillboards = (
  billboards: Billboard[]
): OptionsSelect[] => {
  if (billboards.length === 0) return [];

  // Filter billboards that are today or in the future
  const billboardsToday = billboards.filter(
    (billboard) => new Date(billboard.date) >= new Date()
  );

  console.log(
    billboardsToday.map((billboard) => ({
      value: billboard.id,
      label: `${billboard.movie?.name} || ${
        billboard.room?.name
      } || ${formatDate(billboard.date)} ${formatTime(billboard.startTime)}`,
    }))
  );

  return billboardsToday.map((billboard) => ({
    value: billboard.id,
    label: `${billboard.movie?.name} || ${billboard.room?.name} || ${formatDate(
      billboard.date
    )} ${formatTime(billboard.startTime)}`,
  }));
};
