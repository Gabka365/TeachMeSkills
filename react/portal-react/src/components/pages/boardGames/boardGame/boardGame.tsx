import { FC } from "react";
import "./boardGame.css";

interface BoardGameProp {
  i: number;
  id: number;
  title: string;
  countOfUserWhoLikeIt: number;
}

const BoardGame: FC<BoardGameProp> = ({
  i,
  id,
  title,
  countOfUserWhoLikeIt,
}) => {
  return (
    <li className="board-game-item top-board-game-item">
      <input className="top-board-game-id" type="hidden" value={id} />
      <p>{i}</p>
      <a className="board-game-title text-dark top-board-game-title" href="#">
        {title}
      </a>
      <p className="like-count">{countOfUserWhoLikeIt}</p>
    </li>
  );
};

export default BoardGame;
