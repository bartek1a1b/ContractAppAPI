import { Role } from "./role"


export interface AppUser {
    id: number
    email: string
    firstName: string
    lastName: string
    roleName: string
    role: Role
  }