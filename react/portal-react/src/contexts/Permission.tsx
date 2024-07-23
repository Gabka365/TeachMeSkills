import { PropsWithChildren } from 'react';
import { useAuthContext } from './AuthContext';
import { PermissionType } from '../models/PremisionsType';

interface PermissionProps {
    check: (obj: PermissionType) => boolean;
}

function Permission({ check, children }: PropsWithChildren<PermissionProps>) {
    const { user } = useAuthContext();
    return <>{check(user.permissions) && children}</>;
}

export default Permission;
