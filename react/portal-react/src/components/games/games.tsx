import { useEffect, useState } from 'react';
import './games.css'
import Game from '../game/game';
import gameRepository from '../../repositories/gameRepository';
import GameModel from '../../models/GameModel';

function Games() {
	const { getAll } = gameRepository
	const [games, setGames] = useState<GameModel[]>([])

	console.log('+');

	useEffect(() => {
		// Do it once when we create component
		// DO NOT Do it when we rerender component
		getAll()
			.then(games => {
				setGames(games.data)
			});
	}, [])

	return (
		<div className='games'>
			{games.map(game => 
				<Game 
					name={game.name} 
					yearOfRelease={game.yearOfRelease} 
					key={game.id}></Game>)}
		</div>
	)
}

export default Games;