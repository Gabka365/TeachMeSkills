import { useCallback, useState } from "react";
import { movieRepository } from "../../../repositories";
import MovieModel from "../../../models/MovieModel";
import { useNavigate } from "react-router-dom";


function CreateMovie() {
    const [movie, setMovie] = useState<MovieModel>({} as MovieModel);
    const { add } = movieRepository;
    let navigate = useNavigate();

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

    const onCreate = useCallback(() => {
        add(movie as MovieModel).then((answer) => {
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

            <button onClick={onCreate}>Create</button>
        </div>
    );
}

export default CreateMovie;