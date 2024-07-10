import { createContext, PropsWithChildren, useCallback, useContext, useState } from 'react';
import User from '../models/User';
import { AuthContextData } from './AuthContextData';

const defaultUser = {
    name: 'Guest',
    age: 0,
} as User;

const authContextObjDefault = {
    currentUser: defaultUser,
} as AuthContextData;

const AuthContext = createContext<AuthContextData>(
    authContextObjDefault
);

function ContextProvider({ children }: PropsWithChildren) {
    const [authContextObj, setAuthContextObj] = useState(authContextObjDefault);

    const updateUser = useCallback((user: User) => {
        setAuthContextObj((x) => ({ ...x, currentUser: user }));
    }, []);

    return (
        <AuthContext.Provider
            value={{
                ...authContextObj,
                updateUser,
            }}
        >
            {children}
        </AuthContext.Provider>
    );
}

export function useAuthContext(){
	return useContext(AuthContext)
}

export default ContextProvider;
