import axios from 'axios';
import FavoriteBoardGameIndexViewModel from '../models/boardGames/FavoriteBoardGameIndexViewModel';
import BoardGameIndexViewModel from '../models/boardGames/BoardGameIndexViewModel';
import { BASE_API_URL } from './apiConstatns';

const BOARD_GAME_API_URL = `${BASE_API_URL}api/BoardGame/`;

function getTop3() {
    return axios.get<FavoriteBoardGameIndexViewModel[]>(
        `${BOARD_GAME_API_URL}GetTop3`
    );
}

function getAll() {
    return axios.get<BoardGameIndexViewModel[]>(`${BOARD_GAME_API_URL}GetAll`);
}

function get(id: number) {
    return axios.get(`${BOARD_GAME_API_URL}Get?id=${id}`);
}

const boardGameRepository = {
    getTop3,
    getAll,
    get,
};

export default boardGameRepository;
