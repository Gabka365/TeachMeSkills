import { FC } from 'react';
import './movie.css';
import MovieModel from '../../../../models/MovieModel';

interface MovieProp {
    movie: MovieModel;
}

const Movie: FC<MovieProp> = ({movie}) => {

    return (
        <div className="movie">
            Movie name: {movie.name} release in {movie.releaseYear} directed by {movie.director}
        </div>
    );
}

export default Movie;