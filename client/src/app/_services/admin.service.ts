import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AppUser } from '../_models/appUser';
import { Observable } from 'rxjs';

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

  deleteUser(id: number) {
    return this.http.delete(this.baseUrl + 'users/delete-user/' + id);
  }
}
