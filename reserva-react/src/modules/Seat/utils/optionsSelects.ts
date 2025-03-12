import { Room } from "@/modules/Room/schemas/RoomSchema";

export interface OptionsSelect {
  value: number;
  label: string;
}

export const optionsToSelect = (rooms: Room[]): OptionsSelect[] => {
  return rooms.map((room) => ({
    value: room.id,
    label: room.name,
  }));
};
