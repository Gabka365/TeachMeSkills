import { FC, useCallback, useState } from 'react';
import './game.css';
import { Link } from 'react-router-dom';
import GameModel from '../../../../models/GameModel';
import { gameRepository } from '../../../../repositories';
import Permission from '../../../../contexts/Permission';

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
            <Permission check={(p) => p.CanDeleteGame && p.CanCreateAndUpdateBoardGames || p.CanDeleteBoardGames}>
                <button onClick={() => removeGame(game.id)}>Remove</button>
            </Permission>
        </div>
    );
};

export default Game;
