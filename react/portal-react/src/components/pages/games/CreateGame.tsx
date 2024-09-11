import { useCallback, useState } from 'react';
import { gameRepository } from '../../../repositories';
import GameModel from '../../../models/GameModel';
import { useNavigate } from 'react-router-dom';

function CreateGame() {
    let navigate = useNavigate();
    const { add } = gameRepository;
    const [name, setName] = useState<string>('Half-Life');
    const [yearOfRelease, setYearOfRelease] = useState<number>(2001);

    const onNameChange = useCallback(
        (e: React.ChangeEvent<HTMLInputElement>) => {
            setName(e.target.value);
        },
        []
    );

    const onYearOfReleaseChange = useCallback(
        (e: React.ChangeEvent<HTMLInputElement>) => {
            setYearOfRelease(+e.target.value);
        },
        []
    );

    const onCreate = useCallback(() => {
        add({ name, yearOfRelease } as GameModel).then((answer) => {
            if (answer.data) {
                navigate('/game');
            } else {
                console.log('error');
            }
        });
    }, [name, yearOfRelease]);

    return (
        <div>
            <div>
                name:
                <input type="text" value={name} onChange={onNameChange} />
            </div>
            <div>
                YearOfRelease:
                <input
                    type="number"
                    value={yearOfRelease}
                    onChange={onYearOfReleaseChange}
                />
            </div>
            <button onClick={onCreate}>Create</button>
        </div>
    );
}

export default CreateGame;
