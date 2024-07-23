import { PermissionType } from "./PremisionsType";

export interface ApiUser {
    userName: string;
    isAdmin: boolean;
    permissions: PermissionType;
}
