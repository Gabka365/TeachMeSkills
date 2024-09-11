import { useCallback, useEffect, useState } from 'react';
import './games.css';
import Game from './game/game';
import gameRepository from '../../../repositories/gameRepository';
import GameModel from '../../../models/GameModel';
import { Link } from 'react-router-dom';

function Games() {
    const { getAll } = gameRepository;
    const [games, setGames] = useState<GameModel[]>([]);

    useEffect(() => {
        // Do it once when we create component
        // DO NOT Do it when we rerender component
        getAll().then((games) => {
            setGames(games.data);
        });
    }, []);

    const onGameDelete = useCallback((id: number) => {
        setGames((oldGames) => [...oldGames.filter((g) => g.id !== id)]);
    }, []);

    return (
        <div className="games">
            <Link to={'/game/create'}>Create game</Link>
            {games.map((game) => (
                <Game game={game} onDelete={onGameDelete} key={game.id}></Game>
            ))}
        </div>
    );
}

export default Games;
