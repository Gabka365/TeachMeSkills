import { useCallback, useEffect, useState } from "react";
import MovieModel from "../../../models/MovieModel";
import { movieRepository } from "../../../repositories";
import { useNavigate, useParams } from "react-router-dom";

function UpdateMovie() {
    const {movieId} = useParams();
    const { get, update } = movieRepository;
    const [movie, setMovie] = useState<MovieModel>({} as MovieModel);
    let navigate = useNavigate();

    useEffect(() => {
        get(+movieId!).then((response) => {
            setMovie(response.data as MovieModel);
        });
    }, []);

    const onNameChange = useCallback(
        (e: React.ChangeEvent<HTMLInputElement>) => {
            setMovie(oldMovie => {
                return {...oldMovie, name: e.target.value}
            });
        },
        []
    );

    const onReleaseYearChange = useCallback(
        (e: React.ChangeEvent<HTMLInputElement>) => {
            setMovie(oldMovie => {
                return {...oldMovie, releaseYear: +e.target.value}
            });
        },
        []
    );

    const onDirectorChange = useCallback(
        (e: React.ChangeEvent<HTMLInputElement>) => {
            setMovie(oldMovie => {
                return {...oldMovie, director: e.target.value}
            });
        },
        []
    );

    const onBudgetChange = useCallback(
        (e: React.ChangeEvent<HTMLInputElement>) => {
            setMovie(oldMovie => {
                return {...oldMovie, budget: +e.target.value}
            });
        },
        []
    );

    const onCountryChange = useCallback(
        (e: React.ChangeEvent<HTMLInputElement>) => {
            setMovie(oldMovie => {
                return {...oldMovie, countryOfOrigin: e.target.value}
            });
        },
        []
    );

    const onDescChange = useCallback(
        (e: React.ChangeEvent<HTMLInputElement>) => {
            setMovie(oldMovie => {
                return {...oldMovie, description: e.target.value}
            });
        },
        []
    );

    const onUpdate = useCallback(() => {
        update(movie as MovieModel).then((answer) => {
            if (answer.data) {
                navigate('/movies');
            } else {
                console.log('error');
            }
        });
    }, [movie]);

    return (
        <div>
            <div>
                Name:
                <input type="text" value={movie.name} onChange={onNameChange} />
            </div>
            <div>
                ReleaseYear:
                <input type="number" value={movie.releaseYear} onChange={onReleaseYearChange} />
            </div>
            <div>
                Description:
                <input type="text" value={movie.description} onChange={onDescChange} />
            </div>
            <div>
                Director:
                <input type="text" value={movie.director} onChange={onDirectorChange} />
            </div>
            <div>
                Budget:
                <input type="number" value={movie.budget} onChange={onBudgetChange} />
            </div>
            <div>
                Country:
                <input type="text" value={movie.countryOfOrigin} onChange={onCountryChange} />
            </div>
            <div>
                <input type="hidden" value={movieId} />
            </div>
            <button onClick={onUpdate}>Update</button>
        </div>
    );
}

export default UpdateMovie;