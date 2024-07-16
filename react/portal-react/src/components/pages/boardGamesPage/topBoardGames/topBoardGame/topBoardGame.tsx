import { FC } from 'react';
import './topBoardGame.css';

interface TopBoardGameProp {
    index: number;
    id: number;
    title: string;
    countOfUserWhoLikeIt: number;
}

const TopBoardGame: FC<TopBoardGameProp> = ({
    index,
    id,
    title,
    countOfUserWhoLikeIt,
}) => {
    return (
        <li className="board-game-item top-board-game-item">
            <input className="top-board-game-id" type="hidden" value={id} />
            <p>{index}</p>
            <a
                className="board-game-title text-dark top-board-game-title"
                href="#"
            >
                {title}
            </a>
            <p className="like-count">{countOfUserWhoLikeIt}</p>
        </li>
    );
};

export default TopBoardGame;
