import { FC, useCallback, useState } from 'react';
import './movie.css';
import MovieModel from '../../../../models/MovieModel';
import { Link } from 'react-router-dom';
import { movieRepository } from '../../../../repositories';

interface MovieProp {
    movie: MovieModel;
    onDelete: (id: number) => void;
}

const Movie: FC<MovieProp> = ({movie, onDelete}) => {
    const { remove } = movieRepository;
    const [isDeleted, setIsDeleted] = useState(false);
    const removeMovie = useCallback((id: number) => {
        setIsDeleted(true);
        remove(id);
        onDelete(id);
    }, []);

    return (
        <div className={`movie ${isDeleted ? 'deleted' : ''}`}>
            <Link to={`/movies/${movie.id}`}>{movie.name}</Link>
            released in {movie.releaseYear} directed by {movie.director}
            <button onClick={() => removeMovie(movie.id)}>Remove</button>
        </div>
    );
}

export default Movie;