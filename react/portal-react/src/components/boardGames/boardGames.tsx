import { useEffect, useState } from "react";
import BoardGame from "../boardGame/boardGame";
import "./boardGames.css";
import gameRepository from "../../repositories/boardGameRepository";
import FavoriteBoardGameIndexViewModel from "../../models/FavoriteBoardGameIndexViewModel";

function BoardGames() {
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
        <BoardGame
          i={i + 1}
          id={boardGame.id}
          title={boardGame.title}
          countOfUserWhoLikeIt={boardGame.countOfUserWhoLikeIt}
          key={boardGame.id}
        ></BoardGame>
      ))}
    </ul>
  );
}

export default BoardGames;
