import './App.css';
import { BrowserRouter, Link, Route, Routes } from 'react-router-dom';
import { Home } from './components/pages';
import { CreateGame, GameDetails, Games } from './components/pages/games';
import { CreateMovie, MovieDetails, Movies } from './components/pages/movies';
import {
    BoardGamesPage,
    BoardGameDetails,
} from './components/pages/boardGamesPage';

function App() {
    return (
        <div className="App">
            <BrowserRouter>
                <div>
                    <Link to="/">Home</Link>
                    <Link to="/game">Games</Link>
                    <Link to="/boardGame">Board Games</Link>
                    <Link to="/movies">Movies</Link>
                </div>
                <div className="content">
                    <Routes>
                        <Route path="" Component={Home}></Route>
                        <Route path="/game">
                            <Route path=":id" Component={GameDetails}></Route>
                            <Route path="" Component={Games}></Route>
                            <Route path="create" Component={CreateGame}></Route>
                        </Route>

                        <Route path="/boardGame">
                            <Route path=":id" Component={BoardGameDetails}></Route>
                            <Route path="" Component={BoardGamesPage}></Route>
                        </Route>

                        <Route path="/movies">
                            <Route path="" Component={Movies}></Route>
                            <Route path=":id" Component={MovieDetails}></Route>
                            <Route path="create" Component={CreateMovie}></Route>
                        </Route>
                    </Routes>
                </div>
            </BrowserRouter>
        </div>
    );
}

export default App;
