import { useCallback, useState } from "react";
import { movieRepository } from "../../../repositories";
import MovieModel from "../../../models/MovieModel";
import { useNavigate } from "react-router-dom";


function CreateMovie() {
    const [name, setName] = useState<string>();
    const [releaseYear, setReleaseYear] = useState<number>();
    const [director, setDirector] = useState<string>();
    const [budget, setBudget] = useState<number>();
    const [countryOfOrigin, setCountry] = useState<string>();
    const [description, setDescription] = useState<string>();

    const { add } = movieRepository;
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

    const onCreate = useCallback(() => {
        add({ name, releaseYear, director, budget, countryOfOrigin, description} as MovieModel).then((answer) => {
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

            <button onClick={onCreate}>Create</button>
        </div>
    );
}

export default CreateMovie;