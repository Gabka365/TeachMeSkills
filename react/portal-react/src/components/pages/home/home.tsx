import { useCallback } from 'react';
import { useAuthContext } from '../../../contexts/AuthContext';

function Home() {
    const authContext = useAuthContext();
    const onUserNameChange = useCallback(
        (e: React.ChangeEvent<HTMLInputElement>) => {
            const newName = e.target.value;
            const oldUser = authContext.currentUser;
            authContext.updateUser({ ...oldUser, name: newName });
        },
        []
    );

    return (
        <div>
            <div>Hi {authContext.currentUser.name}</div>
            <input value={authContext.currentUser.name} onChange={onUserNameChange} />
        </div>
    );
}

export default Home;
