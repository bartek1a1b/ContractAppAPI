import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  roles: any;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getRoles();
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  getRoles() {
    this.http.get('https://localhost:7255/api/role').subscribe({
      next: response => this.roles = response,
      error: error => console.log(error),
      complete: () => console.log('Request has completed')
    })
  }

  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }
}
