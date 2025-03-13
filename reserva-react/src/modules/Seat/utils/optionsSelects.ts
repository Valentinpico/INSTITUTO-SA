import { Movie } from "@/modules/Movie/schemas/MovieSchema";
import { Room } from "@/modules/Room/schemas/RoomSchema";

export interface OptionsSelect {
  value: number;
  label: string;
}

export const optionsToSelect = (rooms: Room[] | Movie[]): OptionsSelect[] => {
  return rooms.map((room) => ({
    value: room.id,
    label: room.name,
  }));
};
