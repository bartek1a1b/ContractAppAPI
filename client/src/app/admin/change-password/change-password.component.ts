import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/_services/account.service';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {
  model: any = {};

  constructor(public accountService: AccountService, private router: Router, private toastr: ToastrService) {}

  ngOnInit(): void {
    
  }

  changePassword() {
    this.accountService.changePassword(this.model).subscribe({
      next: _ => {
        this.toastr.success('Hasło zostało zmienione');
        this.router.navigateByUrl('/users');
      },
      error: err => {
        this.toastr.error('Wystąpił błąd podczas zmiany hasła');
        console.error(err);
      }
    });
  }

}
