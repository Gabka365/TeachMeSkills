import axios from 'axios';
import GameModel from '../models/GameModel';
import { BASE_API_URL } from './apiConstatns';
import { json } from 'react-router-dom';
import PostModel from '../models/PostModel';


const BLOG_API_URL = `${BASE_API_URL}api/blog/`;

function getAll(){
    return axios.get<PostModel[]>(`${BLOG_API_URL}GetAll`)
}

const blogRepository = {
    getAll
}

export default blogRepository