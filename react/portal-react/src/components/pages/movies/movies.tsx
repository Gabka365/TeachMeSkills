import { useCallback, useEffect, useState } from 'react';
import './movies.css';
import Movie from './movie/movie';
import MovieModel from '../../../models/MovieModel';
import { movieRepository } from '../../../repositories';
import { Link } from 'react-router-dom';
import { useAuthContext } from '../../../contexts/AuthContext';

function Movies() {
    const {getAll} = movieRepository;
    const [movies, setMovies] = useState<MovieModel[]>([]);
    const {user} = useAuthContext();

    useEffect(() => {
        getAll()
        .then(movies => {
            setMovies(movies.data);
        });
    }, []);

    const onMovieDelete = useCallback((id: number) => {
        setMovies((oldListMovies) => [...oldListMovies.filter((movie) => movie.id !== id)]);
    }, []);

    return (
        <div className="movies">
            <>{user.isAdmin && <Link to={'/movies/create'}>Create movie</Link>}</>
            {movies.map((movie) => (
                <Movie movie={movie} onDelete={onMovieDelete} key={movie.id}></Movie>
            ))}
        </div>
    );
}

export default Movies;