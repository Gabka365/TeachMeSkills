import { FC } from 'react';
import { BASE_API_URL } from '../../../../../repositories/apiConstatns';
import './boardGame.css';

interface BoardGameProp {
    id: number;
    title: string;
}

const BoardGame: FC<BoardGameProp> = ({ id, title }) => {
    return (
        <li className="board-game-item board-game">
            <input className="board-game-id" type="hidden" value={id} />
            <a className="board-game-title text-dark" href="#">
                {title}
            </a>
            <div className="update-and-delete">
                <a className="edit-link" href="#">
                    <img
                        className="icon edit-icon"
                        src={`${BASE_API_URL}/images/BoardGame/edit.svg`}
                        alt="Изменить"
                    />
                </a>

                <div className="delete-link">
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
