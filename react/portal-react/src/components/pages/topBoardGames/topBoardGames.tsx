import { useEffect, useState } from 'react';
import TopBoardGame from './topBoardGame/topBoardGame';
import './topBoardGames.css';
import gameRepository from '../../../repositories/boardGameRepository';
import FavoriteBoardGameIndexViewModel from '../../../models/FavoriteBoardGameIndexViewModel';

function TopBoardGames() {
    const { getTop3 } = gameRepository;
    const [boardGames, setBoardGame] = useState<
        FavoriteBoardGameIndexViewModel[]
    >([]);

    useEffect(() => {
        getTop3().then((boardGames) => {
            setBoardGame(boardGames.data);
        });
    });

    return (
        <ul className="board-games top-board-games">
            {boardGames.map((boardGame, i) => (
                <TopBoardGame
                    index={i + 1}
                    id={boardGame.id}
                    title={boardGame.title}
                    countOfUserWhoLikeIt={boardGame.countOfUserWhoLikeIt}
                    key={boardGame.id}
                ></TopBoardGame>
            ))}
        </ul>
    );
}

export default TopBoardGames;
