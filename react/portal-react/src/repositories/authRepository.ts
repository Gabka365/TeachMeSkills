import axios from 'axios';
import { BASE_API_URL } from './apiConstatns';
import { AuthUserData } from '../models/AuthUserData';

const AUTH_API_URL = `${BASE_API_URL}api/auth/`;

function login(name: string, password: string) {
    return axios.get<AuthUserData>(`${AUTH_API_URL}login?name=${name}&password=${password}`);
}

const authRepository = {
    login,
};

export default authRepository;
