import { useEffect, useState } from 'react';
import './movies.css';
import Movie from './movie/movie';
import movieRepository from '../../../repositories/movieRepository';
import MovieModel from '../../../models/MovieModel';

function Movies() {
    const {getAll} = movieRepository;
    const [movies, setMovies] = useState<MovieModel[]>([]);

    useEffect(() => {
        getAll()
        .then(movies => {
            setMovies(movies.data);
        });
    }, []);

    return (
        <div className="movies">
            {movies.map(movie => (
                <Movie movie={movie} key={movie.id}></Movie>
            ))}
        </div>
    );
}

export default Movies;