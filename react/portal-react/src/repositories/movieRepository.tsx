import axios from "axios";
import { BASE_API_URL } from "./apiConstatns";
import MovieModel from "../models/MovieModel";

const MOVIE_API_URL = `${BASE_API_URL}api/movie/`;

function getAll() {
    return axios.get<MovieModel[]>(`${MOVIE_API_URL}GetAll`);
}

const movieRepository = {
    getAll,
};

export default movieRepository;