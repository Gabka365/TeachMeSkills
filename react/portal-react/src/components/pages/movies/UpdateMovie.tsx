import { useCallback, useEffect, useState } from "react";
import MovieModel from "../../../models/MovieModel";
import { movieRepository } from "../../../repositories";
import { useNavigate, useParams } from "react-router-dom";

function UpdateMovie() {
    const {movieId} = useParams();
    const { get } = movieRepository;
    const [movie, setMovie] = useState<MovieModel>();

    useEffect(() => {
        if (movieId) {
            get(+movieId).then((response) => {
                setMovie(response.data as MovieModel);
            });
        } else {
            console.error('There is no ID');
        }
    }, []);

    const [name, setName] = useState(movie?.name);
    const [releaseYear, setReleaseYear] = useState(movie?.releaseYear);
    const [director, setDirector] = useState(movie?.director);
    const [budget, setBudget] = useState(movie?.budget);
    const [countryOfOrigin, setCountry] = useState(movie?.countryOfOrigin);
    const [description, setDescription] = useState(movie?.description);
    const [id, setMovieid] = useState<number>(+movieId!);
    const { update } = movieRepository;
    let navigate = useNavigate();

    const onNameChange = useCallback(
        (e: React.ChangeEvent<HTMLInputElement>) => {
            setName(e.target.value);
        },
        []
    );

    const onReleaseYearChange = useCallback(
        (e: React.ChangeEvent<HTMLInputElement>) => {
            setReleaseYear(+e.target.value);
        },
        []
    );

    const onDirectorChange = useCallback(
        (e: React.ChangeEvent<HTMLInputElement>) => {
            setDirector(e.target.value);
        },
        []
    );

    const onBudgetChange = useCallback(
        (e: React.ChangeEvent<HTMLInputElement>) => {
            setBudget(+e.target.value);
        },
        []
    );

    const onCountryChange = useCallback(
        (e: React.ChangeEvent<HTMLInputElement>) => {
            setCountry(e.target.value);
        },
        []
    );

    const onDescChange = useCallback(
        (e: React.ChangeEvent<HTMLInputElement>) => {
            setDescription(e.target.value);
        },
        []
    ); 
    
    const onIdChange = useCallback(
        (e: React.ChangeEvent<HTMLInputElement>) => {
            setMovieid(+e.target.value);
        },
        []
    );

    const onUpdate = useCallback(() => {
        update({ id, name, releaseYear, director, budget, countryOfOrigin, description} as MovieModel).then((answer) => {
            if (answer.data) {
                navigate('/movies');
            } else {
                console.log('error');
            }
        });
    }, [name, releaseYear, director, budget, countryOfOrigin, description]);
    return (
        <div>
            <div>
                Name:
                <input type="text" value={name} onChange={onNameChange} />
            </div>
            <div>
                ReleaseYear:
                <input type="number" value={releaseYear} onChange={onReleaseYearChange} />
            </div>
            <div>
                Description:
                <input type="text" value={description} onChange={onDescChange} />
            </div>
            <div>
                Director:
                <input type="text" value={director} onChange={onDirectorChange} />
            </div>
            <div>
                Budget:
                <input type="number" value={budget} onChange={onBudgetChange} />
            </div>
            <div>
                Country:
                <input type="text" value={countryOfOrigin} onChange={onCountryChange} />
            </div>
            <div>
                <input type="hidden" value={id} onChange={onIdChange}/>
            </div>
            <button onClick={onUpdate}>Update</button>
        </div>
    );
}

export default UpdateMovie;