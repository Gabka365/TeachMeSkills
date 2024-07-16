import {
    createContext,
    PropsWithChildren,
    useCallback,
    useContext,
    useState,
} from 'react';
import { ApiUser } from '../models/ApiUser';

interface AuthContextData {
    user: ApiUser;
    setUser: (user: ApiUser) => void;
}

const AuthContext = createContext<AuthContextData>({} as AuthContextData);

function ContextProvider({ children }: PropsWithChildren) {
    const [authData, setAuthData] = useState<AuthContextData>(
        {} as AuthContextData
    );

    const setUser = useCallback((user: ApiUser) => {
        setAuthData((oldAuthData) => {
            return { ...oldAuthData, user };
        });
    }, []);

    return (
        <AuthContext.Provider value={{ ...authData, setUser }}>
            {children}
        </AuthContext.Provider>
    );
}

export default ContextProvider;

export function useAuthContext() {
    return useContext(AuthContext);
}
