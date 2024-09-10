import axios from 'axios';
import GameModel from '../models/GameModel';
import { BASE_API_URL } from './apiConstatns';
import { json } from 'react-router-dom';
import PostModel from '../models/PostModel';


const BLOG_API_URL = `${BASE_API_URL}api/blog/`;

function getAll(){
    return axios.get<PostModel[]>(`${BLOG_API_URL}GetAll`)
}


function remove(id: number){
    return axios.get(`${BLOG_API_URL}Remove?id=${id}`)
}


function get(id: number){
    return axios.get(`${BLOG_API_URL}Get?id=${id}`)
}


function add(game: PostModel) {
    return axios.post(`${BLOG_API_URL}create`, game);
}


function update(movie: PostModel) {
    return axios.post(`${BLOG_API_URL}update`, movie);
}


const blogRepository = {
    getAll,
    remove,
    add,
    get,
    update
}

export default blogRepository