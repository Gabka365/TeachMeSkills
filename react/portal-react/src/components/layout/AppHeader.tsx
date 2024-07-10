import { Link } from "react-router-dom";

function AppHeader() {
	return (
		<div>
			<Link to='/'>Home</Link>
			<Link to='/game'>Games</Link>
			<Link to='/game/3'>Game number 3</Link>
		</div>
	);
}

export default AppHeader;