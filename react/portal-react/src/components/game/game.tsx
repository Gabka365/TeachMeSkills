import { FC } from 'react';
import './game.css';
import GameModel from '../../models/GameModel';
import { Link } from 'react-router-dom';

interface GameProp {
    game: GameModel;
}

const Game: FC<GameProp> = ({ game }) => {
    return (
        <div className="game">
            <Link to={`/game/${game.id}`}>{game.name}</Link>
            release in
            {game.yearOfRelease}
        </div>
    );
};

export default Game;
