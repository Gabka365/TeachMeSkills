import { useAuthContext } from '../../../contexts/AuthContext';

function Home() {
    const authData = useAuthContext();

    return (
        <div>
            {authData.user ? (
                <div>
                    <div>Hi {authData.user.userName}</div>
                    <div>IsAdmin: {authData.user.isAdmin + ''}</div>
                </div>
            ) : (
                <div>Привет гость</div>
            )}
        </div>
    );
}

export default Home;
