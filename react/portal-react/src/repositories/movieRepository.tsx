import axios from "axios";
import { BASE_API_URL } from "./apiConstatns";
import MovieModel from "../models/MovieModel";

const MOVIE_API_URL = `${BASE_API_URL}api/movie/`;

function getAll() {
    return axios.get<MovieModel[]>(`${MOVIE_API_URL}GetAll`);
}

function get(id: number) {
    return axios.get(`${MOVIE_API_URL}get?id=${id}`);
}

function remove (id: number) {
    return axios.get(`${MOVIE_API_URL}remove?id=${id}`);
}

function add(movie: MovieModel) {
    return axios.post(`${MOVIE_API_URL}create`, movie);
}

function update(movie: MovieModel) {
    return axios.post(`${MOVIE_API_URL}update`, movie);
}

const movieRepository = {
    getAll,
    get,
    remove,
    add,
    update,
}

export default movieRepository;