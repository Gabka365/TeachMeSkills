import { useAuthContext } from '../../../contexts/AuthContext';

function Home() {
    const { user } = useAuthContext();

    return (
        <div>
            {user ? (
                <div>
                    <div>Hi {user.userName}</div>
                    <div>IsAdmin: {user.isAdmin + ''}</div>
                </div>
            ) : (
                <div>Привет гость</div>
            )}
        </div>
    );
}

export default Home;
