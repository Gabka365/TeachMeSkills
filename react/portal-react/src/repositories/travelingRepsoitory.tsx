import axios from "axios";
import { NEWS_API_URL } from "./apiConstatns";
import { json } from 'react-router-dom';
import NewsModel from '../models/NewsModel';

function getLastNews() {
    return axios.get<NewsModel>(`${NEWS_API_URL}DtoLastNews`);
}
const travelingRepsoitory = {
    getLastNews
};
export default travelingRepsoitory;