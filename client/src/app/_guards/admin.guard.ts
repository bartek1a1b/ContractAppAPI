import { inject } from '@angular/core';
import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';
import { map } from 'rxjs';

export const adminGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const toastr = inject(ToastrService);

  return accountService.currentUser$.pipe(
    map(user => {
      console.log(user);
      if (!user) return false;
      if (user.roles.includes('Admin')) {
        return true;
      } else {
        toastr.error("Nie masz wystarczających uprawnień");
        return false;
      }
    })
  )

};
