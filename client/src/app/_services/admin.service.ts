import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AppUser } from '../_models/appUser';
import { Observable } from 'rxjs';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = environment.apiUrl;
  users: AppUser[] = [];

  constructor(private http: HttpClient) { }

  getUsers(): Observable<AppUser[]> {
    const url = `${this.baseUrl}users`;
    return this.http.get<AppUser[]>(url);
  }

  getUsersWithRoles() {
    return this.http.get<User[]>(this.baseUrl + "admin/users-with-roles");
  }

  updateUserRoles(username: string, roles: string) {
    return this.http.post<string[]>(this.baseUrl + 'admin/edit-roles/' + username + '?roles=' + roles, {});
  }

  deleteUser(id: number) {
    return this.http.delete(this.baseUrl + 'users/delete-user/' + id);
  }
}
