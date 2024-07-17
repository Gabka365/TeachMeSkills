import './App.css';
import { BrowserRouter, Link, Route, Routes } from 'react-router-dom';
import { Home, Login } from './components/pages';
import { CreateGame, GameDetails, Games } from './components/pages/games';
import { CreateMovie, MovieDetails, Movies } from './components/pages/movies';
import {
    BoardGamesPage,
    BoardGameDetails,
    CreateBoardGame,
} from './components/pages/boardGamesPage';
import AuthContext from './contexts/AuthContext';

function App() {
    return (
        <div className="App">
            <AuthContext>
                <BrowserRouter>
                    <div>
                        <Link to="/">Home</Link>
                        <Link to="/login">Login</Link>
                        <Link to="/game">Games</Link>
                        <Link to="/boardGame">Board Games</Link>
                        <Link to="/movies">Movies</Link>
                    </div>
                    <div className="content">
                        <Routes>
                            <Route path="" Component={Home}></Route>
                            <Route path="/login" Component={Login}></Route>
                            <Route path="/game">
                                <Route
                                    path=":id"
                                    Component={GameDetails}
                                ></Route>
                                <Route path="" Component={Games}></Route>
                                <Route
                                    path="create"
                                    Component={CreateGame}
                                ></Route>
                            </Route>

                            <Route path="/boardGame">
                                <Route
                                    path=":id"
                                    Component={BoardGameDetails}
                                ></Route>
                                <Route
                                    path=""
                                    Component={BoardGamesPage}
                                ></Route>
                                <Route
                                    path="create"
                                    Component={CreateBoardGame}
                                ></Route>
                            </Route>

                            <Route path="/movies">
                                <Route path="" Component={Movies}></Route>
                                <Route
                                    path=":id"
                                    Component={MovieDetails}
                                ></Route>
                                <Route
                                    path="create"
                                    Component={CreateMovie}
                                ></Route>
                            </Route>
                        </Routes>
                    </div>
                </BrowserRouter>
            </AuthContext>
        </div>
    );
}

export default App;
