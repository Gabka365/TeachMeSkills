import User from "../models/User";

export interface AuthContextData {
    currentUser: User;
    updateUser: (user: User) => void;
}