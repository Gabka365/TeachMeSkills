import axios from "axios";
import { NEWS_API_URL, BASE_API_URL } from "./apiConstatns";
import NewsModel from '../models/newsModel';
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
function create(data: FormData) {
    return axios.post(`${TRAVELING_API_URL}CreateTravelings`, data);
}

const travelingRepsoitory = {
    getLastNews,
    getAll,
    remove,
    create,
};
export default travelingRepsoitory;