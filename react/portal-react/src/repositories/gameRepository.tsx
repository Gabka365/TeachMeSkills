import axios from 'axios';
import GameModel from '../models/GameModel';
import { BASE_API_URL } from './apiConstatns';
import { json } from 'react-router-dom';

const GAME_API_URL = `${BASE_API_URL}api/games/`;

function getAll() {
    return axios.get<GameModel[]>(`${GAME_API_URL}GetAll`);
}

function get(id: number) {
    return axios.get(`${GAME_API_URL}get?id=${id}`);
}

function remove(id: number) {
    return axios.get(`${GAME_API_URL}remove?id=${id}`);
}

function add(game: GameModel) {
    return axios.post(`${GAME_API_URL}create`, game);
}

const gameRepository = {
    getAll,
    get,
    remove,
    add,
};

export default gameRepository;
