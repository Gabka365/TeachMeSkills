import { useCallback, useState } from 'react';
import authRepository from '../../../repositories/authRepository';
import { useAuthContext } from '../../../contexts/AuthContext';
import { ApiUser } from '../../../models/ApiUser';
import { PermissionType } from '../../../models/PremisionsType';

function Login() {
    const { setUser } = useAuthContext();
    const { login } = authRepository;
    const [name, setName] = useState('');
    const [password, setPassword] = useState('');
    const onLogin = useCallback(() => {
        login(name, password).then((data) => {
            const user = data.data;
			const permission = {} as any;
			user.permissions.split(', ').forEach((permissionStr) => {
				permission[permissionStr] = true
			})
            const apiUser = {
                userName: user.name,
                isAdmin: user.roles.split(', ').includes('Admin'),
                permissions: permission as PermissionType,
            } as ApiUser;
            setUser(apiUser);
        });
    }, [name, password]);
    return (
        <div>
            <input value={name} onChange={(e) => setName(e.target.value)} />
            <input
                value={password}
                onChange={(e) => setPassword(e.target.value)}
            />
            <button onClick={onLogin}>Login</button>
            <div>name:{name}</div>
            <div>password:{password}</div>
        </div>
    );
}

export default Login;
