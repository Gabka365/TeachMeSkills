import { useParams } from "react-router-dom";

function GameDetails() {
	let { id } = useParams();

	return (
		<div>
			Details {id}
		</div>
	)
}

export default GameDetails