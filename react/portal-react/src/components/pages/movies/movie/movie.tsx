import { FC, useCallback, useState } from 'react';
import './movie.css';
import MovieModel from '../../../../models/MovieModel';
import { Link } from 'react-router-dom';
import { movieRepository } from '../../../../repositories';
import Permission from '../../../../contexts/Permission';

interface MovieProp {
    movie: MovieModel;
    onDelete: (id: number) => void;
}

const Movie: FC<MovieProp> = ({movie, onDelete}) => {
    const { remove } = movieRepository;
    const removeMovie = useCallback((id: number) => {
        remove(id);
        onDelete(id);
    }, []);

    return (
        <div>
            <Link to={`/movies/${movie.id}`}>{movie.name}</Link>
            released in {movie.releaseYear} directed by {movie.director}

            <Permission check={(p) => p.CanDeleteMovie}>
                <button onClick={() => removeMovie(movie.id)}>Remove</button>
            </Permission>

            <Permission check={(p) => p.CanUpdateMovie}>
                <Link to={`/movies/update/${movie.id}`}>Update</Link>
            </Permission>
        </div>
    );
}

export default Movie;