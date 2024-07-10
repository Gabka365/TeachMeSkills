import { FC } from 'react';
import { Route, Routes } from 'react-router-dom';
import { GameDetails, Games, Home } from './pages';

interface AppRoutesProps {}

const AppRoutes: FC<AppRoutesProps> = () => {
    return (
        <Routes>
            <Route path="/" Component={Home}></Route>
            <Route path="/game">
                <Route path="" Component={Games}></Route>
                <Route path=":id" Component={GameDetails}></Route>
            </Route>
        </Routes>
    );
};

export default AppRoutes;
