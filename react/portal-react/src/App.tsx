import './App.css';
import { BrowserRouter, Link, Route, Routes } from 'react-router-dom';
import { BoardGames, Home } from './components/pages';
import { CreateGame, GameDetails, Games } from './components/pages/games';
import { Travelings, СreateTravelings } from './components/pages/travelings/Index';



function App() {
    return (
        <div className="App">
            <BrowserRouter>
                <div>
                    <Link to="/">Home</Link>
                    <Link to="/game">Games</Link>
                    <Link to="/boardGames">Board Games</Link>
                    <Link to="/traveling">Traveling</Link>
                </div>
                <div className="content">
                    <Routes>
                        <Route path="" Component={Home}></Route>
                        <Route path="/game">
                            <Route path=":id" Component={GameDetails}></Route>
                            <Route path="" Component={Games}></Route>
                            <Route path="create" Component={CreateGame}></Route>
                        </Route>

                        <Route
                            path="/boardGames"
                            Component={BoardGames}
                        ></Route>

                        <Route path="/traveling">
                            <Route path="" Component={Travelings}></Route>
                            <Route
                                path="create"
                                Component={СreateTravelings}
                            ></Route>
                        </Route>
                    </Routes>
                </div>
            </BrowserRouter>
        </div>
    );
}

export default App;
