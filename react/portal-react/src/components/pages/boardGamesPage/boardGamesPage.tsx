import BoardGames from './boardGames/boardGames';
import TopBoardGames from './topBoardGames/topBoardGames';
import './boardGamesPage.css';

function BoardGamesPage() {
    return (
        <div className="board-game-list">
            <BoardGames></BoardGames>
            <TopBoardGames></TopBoardGames>
        </div>
    );
}

export default BoardGamesPage;
