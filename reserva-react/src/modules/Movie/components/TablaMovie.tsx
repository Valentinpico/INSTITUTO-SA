import { TablaDinamica } from "@/components/common/TablaDinamica";
import { getColumsMovieTable } from "./ColumnsMovieTable";
import { Button } from "@/components/ui/button";
import { ModalDefault } from "@/components/common/ModalComponent";
import { useEffect, useState } from "react";
import { FormMovie } from "./FormMovie";
import { Movie } from "../schemas/MovieSchema";
import { deleteMovie_api, getMovies_api } from "../api/movie.service";
import { showToast } from "@/adapters/toast/handleToast";
import { useBookingContext } from "@/Context/BookingProvider";

export const TableMovie = () => {
  const { modal, setModal, customerSelected, setMovieSelected } =
    useBookingContext();

  const [data, setData] = useState<Movie[]>([]);
  const [modalEliminar, setModalEliminar] = useState(false);

  const getAllMovie = async () => {
    const res = await getMovies_api();

    if (!res.success) {
      showToast(res.message || "An error occurred", "error");
    }

    setData(res.data || []);
  };

  const handleEdit = (customer: Movie) => {
    setModal(true);
    setMovieSelected(customer);
  };
  const ButtonDelete = (customer: Movie) => {
    setModalEliminar(true);
    setMovieSelected(customer);
  };
  const columns = getColumsMovieTable({
    deleteAction: ButtonDelete,
    editAction: handleEdit,
  });

  const handleCreate = () => {
    setModal(true);
    setMovieSelected(null);
  };

  const handleDelete = async () => {
    const res = await deleteMovie_api(customerSelected?.id || 0);

    showToast(
      res.message || "Movie deleted successfully",
      res.success ? "success" : "error"
    );

    getAllMovie();
    setModalEliminar(false);
  };

  useEffect(() => {
    getAllMovie();
    return () => {
      console.log("TableMovie unmounted");
    };
  }, []);
  return (
    <div className="container mx-auto py-5">
      <Button
        variant={"outline"}
        className="mb-2 p-5 hover:cursor-pointer"
        onClick={handleCreate}
      >
        New Movie
      </Button>
      <TablaDinamica columns={columns} data={data} />
      <ModalDefault modal={modal} setModal={setModal}>
        <FormMovie getAllMovies={getAllMovie} />
      </ModalDefault>

      <ModalDefault modal={modalEliminar} setModal={setModalEliminar}>
        <h1 className="text-2xl font-bold">Are you sure you want to delete?</h1>
        <p className="text-sm text-gray-500">This action cannot be undone</p>

        <div className="space-y-1 border-t pt-4 mt-4">
          <p>
            <span className="font-semibold">Document Number:</span>
            {customerSelected?.documentNumber}
          </p>
          <p>
            <span className="font-semibold">Name:</span>
            {customerSelected?.name} {customerSelected?.lastname}
          </p>
        </div>
        <div className="flex justify-end space-x-2 border-t pt-4 mt-4">
          <Button variant="outline" onClick={() => setModalEliminar(false)}>
            Cancel
          </Button>
          <Button variant="destructive" onClick={handleDelete}>
            Delete
          </Button>
        </div>
      </ModalDefault>
    </div>
  );
};
