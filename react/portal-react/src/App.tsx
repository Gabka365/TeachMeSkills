import "./App.css";
import Games from "./components/games/games";
import BoardGames from "./components/boardGames/boardGames";

function App() {
  return (
    <div className="App">
      <header className="App-header">
        Smile
        <Games></Games>
        <BoardGames></BoardGames>
        <BoardGames></BoardGames>
      </header>
    </div>
  );
}

export default App;
