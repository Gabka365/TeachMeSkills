import { FC, useCallback, useState } from 'react';
import './game.css';
import { Link } from 'react-router-dom';
import GameModel from '../../../../models/GameModel';
import { gameRepository } from '../../../../repositories';

interface GameProp {
    game: GameModel;
    onDelete: (id: number) => void;
}

const Game: FC<GameProp> = ({ game, onDelete }) => {
    const { remove } = gameRepository;
    const [isDeleted, setIsDeleted] = useState(false);
    const removeGame = useCallback((id: number) => {
        setIsDeleted(true);
        remove(id);
		onDelete(id);
    }, []);

    return (
        <div className={`game ${isDeleted ? 'deleted' : ''}`}>
            <Link to={`/game/${game.id}`}>{game.name} </Link>
            release in {game.yearOfRelease}
            <button onClick={() => removeGame(game.id)}>Remove</button>
        </div>
    );
};

export default Game;
