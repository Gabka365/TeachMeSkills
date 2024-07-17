import axios from 'axios';
import FavoriteBoardGameIndexViewModel from '../models/boardGames/FavoriteBoardGameIndexViewModel';
import BoardGameIndexViewModel from '../models/boardGames/BoardGameIndexViewModel';
import BoardGameCreateViewModel from '../models/boardGames/BoardGameCreateViewModel';
import { BASE_API_URL } from './apiConstatns';

const BOARD_GAME_API_URL = `${BASE_API_URL}api/BoardGame/`;

function add(boardGame: BoardGameCreateViewModel) {
    return axios.post(`${BOARD_GAME_API_URL}Create`, boardGame);
}

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

function remove(id: number) {
    return axios.get(`${BOARD_GAME_API_URL}Delete?id=${id}`);
}

const boardGameRepository = {
    add,
    getTop3,
    getAll,
    get,
    remove,
};

export default boardGameRepository;
