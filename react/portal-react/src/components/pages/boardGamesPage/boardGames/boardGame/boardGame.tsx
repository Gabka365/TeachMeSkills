import { FC, useState, useCallback } from 'react';
import { BASE_API_URL } from '../../../../../repositories/apiConstatns';
import './boardGame.css';
import { Link } from 'react-router-dom';
import boardGameRepository from '../../../../../repositories/boardGameRepository';
import BoardGameIndexViewModel from '../../../../../models/boardGames/BoardGameIndexViewModel';

interface BoardGameProp {
    boardGame: BoardGameIndexViewModel;
    onDelete: (id: number) => void;
}

const BoardGame: FC<BoardGameProp> = ({ boardGame, onDelete }) => {
    const { remove } = boardGameRepository;
    const [isDeleted, setIsDeleted] = useState(false);
    const removeBoardGame = useCallback((id: number) => {
        setIsDeleted(true);
        remove(id);
        onDelete(id);
    }, []);

    return (
        <li className="board-game-item board-game">
            <input
                className="board-game-id"
                type="hidden"
                value={boardGame.id}
            />
            <Link
                className="board-game-title text-dark"
                to={`/boardGame/${boardGame.id}`}
            >
                {boardGame.title}
            </Link>
            <div className="update-and-delete">
                <a className="edit-link" href="#">
                    <img
                        className="icon edit-icon"
                        src={`${BASE_API_URL}/images/BoardGame/edit.svg`}
                        alt="Изменить"
                    />
                </a>

                <div
                    className={`delete-link ${isDeleted ? 'deleted' : ''}`}
                    onClick={() => removeBoardGame(boardGame.id)}
                >
                    <img
                        className="icon delete-icon"
                        src={`${BASE_API_URL}/images/BoardGame/delete.svg`}
                        alt="Удалить"
                    />
                </div>
            </div>
        </li>
    );
};

export default BoardGame;
