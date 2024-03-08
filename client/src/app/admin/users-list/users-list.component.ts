import { Component, Input, OnInit } from '@angular/core';
import { AppUser } from 'src/app/_models/appUser';
import { User } from 'src/app/_models/user';
import { AdminService } from 'src/app/_services/admin.service';

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.css']
})
export class UsersListComponent implements OnInit {
  @Input() user: AppUser | undefined;
  users: AppUser[] = [];

  constructor(private adminService: AdminService) {}

  ngOnInit(): void {
    this.loadUsers();
  }

  loadUsers() {
    this.adminService.getUsers().subscribe({
      next: users => this.users = users
    })
  }

  deleteUser(id: number) {
    this.adminService.deleteUser(id).subscribe({
      next: _ => {
        if (this.user) {
          this.users = this.users.filter(u => u.id !== id);
        }
      },
      complete: () => {
        this.loadUsers();
      }
    })
  }
}
