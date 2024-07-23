import { useCallback, useEffect, useState } from 'react';
import BoardGame from './boardGame/boardGame';
import './boardGames.css';
import gameRepository from '../../../../repositories/boardGameRepository';
import BoardGameIndexViewModel from '../../../../models/boardGames/BoardGameIndexViewModel';
import { Link } from 'react-router-dom';
import Permission from '../../../../contexts/Permission';

function BoardGames() {
    const { getAll } = gameRepository;
    const [boardGames, setBoardGames] = useState<BoardGameIndexViewModel[]>([]);

    useEffect(() => {
        getAll().then((boardGames) => {
            setBoardGames(boardGames.data);
        });
    }, []);

    const onBoardGameDelete = useCallback((id: number) => {
        setBoardGames((oldGames) => [...oldGames.filter((g) => g.id !== id)]);
    }, []);

    return (
        <div>
            <h2 className="title">Список наших настольных игр</h2>
            <ul className="board-games">
                {boardGames.map((boardGame) => (
                    <BoardGame
                        boardGame={boardGame}
                        onDelete={onBoardGameDelete}
                        key={boardGame.id}
                    ></BoardGame>
                ))}
            </ul>
            <div className="favorite-board-game">
                <a className="favorite-board-game-link" href="#">
                    Ваши любимые игры
                </a>
            </div>
            <Permission check={(p) => p.CanCreateAndUpdateBoardGames}>
                <div className="add-board-game">
                    <Link
                        className="add-board-game-link"
                        to="/boardGame/create"
                    >
                        Добавить игру
                    </Link>
                </div>
            </Permission>
        </div>
    );
}

export default BoardGames;
