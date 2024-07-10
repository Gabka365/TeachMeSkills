import axios from "axios";
import FavoriteBoardGameIndexViewModel from "../models/FavoriteBoardGameIndexViewModel";
import { BASE_API_URL } from "./apiConstatns";

const BOARD_GAME_API_URL = `${BASE_API_URL}api/BoardGame/`;

function getTop3(){
  return axios.get<FavoriteBoardGameIndexViewModel[]>(`${BOARD_GAME_API_URL}GetTop3`);
}

const boardGameRepositiry = {
  getTop3
}

export default boardGameRepositiry;