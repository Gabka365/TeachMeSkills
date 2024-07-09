import { FC } from "react"
import './game.css'

interface GameProp {
	name: string,
	yearOfRelease: number
}

const Game: FC<GameProp> = ({ name, yearOfRelease }) => {
	return (
		<div className="game">
			{name} release in {yearOfRelease}
		</div>
	)
}

export default Game