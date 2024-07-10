import { BrowserRouter } from 'react-router-dom';
import './App.css';
import AppRoutes from './components/routes';
import AppHeader from './components/layout/AppHeader';
import AuthContext from './contexts/AuthContext';

function App() {
    return (
        <BrowserRouter>
            <AuthContext>
                <AppHeader></AppHeader>
                <AppRoutes></AppRoutes>
            </AuthContext>
        </BrowserRouter>
    );
}

export default App;
