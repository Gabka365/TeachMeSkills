import axios from "axios";
import { NEWS_API_URL, BASE_API_URL } from "./apiConstatns";
import { json } from 'react-router-dom';
import NewsModel from '../models/NewsModel';
import TravelingModel from '../models/TravelingModel'

const TRAVELING_API_URL = `${BASE_API_URL}api/traveling/`;

function getLastNews() {
    return axios.get<NewsModel>(`${NEWS_API_URL}DtoLastNews`);
}
function getAll() {
    return axios.get<TravelingModel[]>(`${TRAVELING_API_URL}getAll`);
}
function remove(id: number) {
    return axios.get(`${TRAVELING_API_URL}DeletePost?postId=${id}`);
}

const travelingRepsoitory = {
    getLastNews,
    getAll,
    remove,
};
export default travelingRepsoitory;