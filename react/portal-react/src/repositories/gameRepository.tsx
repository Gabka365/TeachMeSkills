import axios from "axios";
import GameModel from "../models/GameModel";

const BASE_API_URL = 'https://localhost:7219/';
const GAME_API_URL = `${BASE_API_URL}api/games/`;

function getAll() {
	return axios.get<GameModel[]>(`${GAME_API_URL}GetAll`)
}

function get(id: number) {
	return axios.get(`${GAME_API_URL}get/${id}`)
}

const gameRepository = {
	getAll,
	get
}

export default gameRepository
