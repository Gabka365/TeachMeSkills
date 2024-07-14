import { useEffect, useState } from 'react';
import BoardGame from './boardGame/boardGame';
import './boardGames.css';
import gameRepository from '../../../../repositories/boardGameRepository';
import BoardGameIndexViewModel from '../../../../models/boardGames/BoardGameIndexViewModel';

function BoardGames() {
    const { getAll } = gameRepository;
    const [boardGames, setBoardGame] = useState<BoardGameIndexViewModel[]>([]);

    useEffect(() => {
        getAll().then((boardGames) => {
            setBoardGame(boardGames.data);
        });
    }, []);

    return (
        <div>
            <h2 className="title">Список наших настольных игр</h2>
            <ul className="board-games">
                {boardGames.map((boardGame) => (
                    <BoardGame
                        id={boardGame.id}
                        title={boardGame.title}
                        key={boardGame.id}
                    ></BoardGame>
                ))}
            </ul>
            <div className="favorite-board-game">
                <a className="favorite-board-game-link" href="#">
                    Ваши любимые игры
                </a>
            </div>
            <div className="add-board-game">
                <a className="add-board-game-link" href="#">
                    Добавить игру
                </a>
            </div>
        </div>
    );
}

export default BoardGames;
