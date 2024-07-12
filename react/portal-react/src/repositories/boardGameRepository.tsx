import axios from 'axios';
import FavoriteBoardGameIndexViewModel from '../models/FavoriteBoardGameIndexViewModel';
import BoardGameIndexViewModel from '../models/BoardGameIndexViewModel';
import { BASE_API_URL } from './apiConstatns';

const BOARD_GAME_API_URL = `${BASE_API_URL}api/BoardGame/`;

function getTop3() {
    return axios.get<FavoriteBoardGameIndexViewModel[]>(
        `${BOARD_GAME_API_URL}GetTop3`
    );
}

function getAll() {
    return axios.get<BoardGameIndexViewModel[]>(
        `${BOARD_GAME_API_URL}GetAll`
    );
}

const boardGameRepository = {
    getTop3,
    getAll
};

export default boardGameRepository;
