import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";
import { Clock, Film, DoorOpen } from "lucide-react";
import { Billboard } from "../schemas/BillboardSchema";
import { Button } from "@/components/ui/button";
import { useBookingContext } from "@/Context/BookingProvider";
import { useEffect, useState } from "react";
import { Movie } from "@/modules/Movie/schemas/MovieSchema";
import { Room } from "@/modules/Room/schemas/RoomSchema";
import { getRoomById_api } from "@/modules/Room/api/room.service";
import { getMovieById_api } from "@/modules/Movie/api/movie.service";
import { MovieGenreEnumLabel } from "@/modules/Movie/utils/enums";

const formatTime = (timeString: string): string => {
  if (!timeString) return "N/A";

  const [hours, minutes] = timeString.split(":");

  const hour = parseInt(hours, 10);
  const ampm = hour >= 12 ? "PM" : "AM";
  const hour12 = hour % 12 || 12;

  return `${hour12}:${minutes} ${ampm}`;
};

const formatDate = (dateString: string) => {
  if (!dateString) return "N/A";

  const date = new Date(dateString);
  return date.toLocaleDateString("en-US", {
    weekday: "long",
    year: "numeric",
    month: "long",
    day: "numeric",
  });
};

type BillboardCardProps = {
  billboard: Billboard;
  buttonDelete: (billboard: Billboard) => void;
  billboards: Billboard[];
};

export const BillboardCard = ({
  billboard,
  buttonDelete,
  billboards,
}: BillboardCardProps) => {
  const { id, status, date, startTime, endTime, movieID, roomID } = billboard;

  const { setModal, setBillboardSelected } = useBookingContext();

  const [room, setRoom] = useState<Room | null>(null);
  const [movie, setMovie] = useState<Movie | null>(null);

  const fetchAllData = async () => {
    const resRoom = getRoomById_api(roomID);
    const resMovie = getMovieById_api(movieID);

    const [room, movie] = await Promise.all([resRoom, resMovie]);

    setRoom(room.data || null);
    setMovie(movie.data || null);
  };

  useEffect(() => {
    fetchAllData();
  }, [billboards]);

  return (
    <Card className="w-full max-w-md shadow-lg hover:shadow-xl transition-shadow duration-300">
      <CardHeader>
        <div className="flex justify-between items-center">
          <CardTitle className="text-xl">Billboard #{id}</CardTitle>
          <Badge variant={status ? "default" : "destructive"}>
            {status ? "Active" : "Inactive"}
          </Badge>
        </div>
        <CardDescription>{formatDate(date)}</CardDescription>
      </CardHeader>

      <CardContent className="space-y-4 p-">
        <div className="flex items-center gap-2">
          <Clock className="h-5 w-5 text-gray-500" />
          <div className="flex flex-col">
            <span className="text-sm text-gray-500">Showtime</span>
            <span className="font-medium">
              {formatTime(startTime)} - {formatTime(endTime)}
            </span>
          </div>
        </div>

        <div className="flex items-center gap-2">
          <Film className="h-5 w-5 text-gray-500" />
          <div className="flex flex-col">
            <span className="text-sm text-gray-500">Movie</span>
            <span className="font-medium">
              {movie?.name},{" "}
              {
                MovieGenreEnumLabel[
                  String(movie?.genre) as keyof typeof MovieGenreEnumLabel
                ]
              }
            </span>
          </div>
        </div>

        <div className="flex items-center gap-2">
          <DoorOpen className="h-5 w-5 text-gray-500" />
          <div className="flex flex-col">
            <span className="text-sm text-gray-500">Theater Room</span>
            <span className="font-medium">
              {room?.name} number: {room?.number}
            </span>
          </div>
        </div>
      </CardContent>

      <CardFooter className="flex justify-end space-x-2 border-t pt-4 ">
        <Button
          variant="outline"
          onClick={() => {
            setModal(true);
            setBillboardSelected(billboard);
          }}
        >
          Edit
        </Button>
        <Button variant="destructive" onClick={() => buttonDelete(billboard)}>
          Delete
        </Button>
      </CardFooter>
    </Card>
  );
};
