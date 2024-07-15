import { useParams } from "react-router-dom";
import { movieRepository } from "../../../repositories";
import { useEffect, useState } from "react";
import MovieModel from "../../../models/MovieModel";
import { BASE_API_URL } from "../../../repositories/apiConstatns";

function MovieDetails() {
    const {id} = useParams();
    const { get } = movieRepository;
    const [movie, setMovie] = useState<MovieModel>();

    useEffect(() => {
        if (id) {
            get(+id).then((response) => {
                setMovie(response.data as MovieModel);
            });
        } else {
            console.error('There is no ID');
        }
    }, []);

    return (<div>
        {!movie && <div>loading</div>}

        {!!movie && (
            <div>
                <img
                    src={`${BASE_API_URL}images/Movie/cover-${movie.id}.jpg`}
                    alt="cover"
                />
                movie name: {movie.name} ({movie.id})
            </div>
        )}
    </div>
    );
}

export default MovieDetails;