import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ContractTypeOneService } from 'src/app/_services/contract-type-one.service';

@Component({
  selector: 'app-contract-type-one-add',
  templateUrl: './contract-type-one-add.component.html',
  styleUrls: ['./contract-type-one-add.component.css']
})
export class ContractTypeOneAddComponent implements OnInit {

  constructor(private contractTypeOneService: ContractTypeOneService, private toastr: ToastrService) {}
  
  ngOnInit(): void {

  }

  onSubmit(form: NgForm) {
    if (form.valid) {
      const contractTypeOneCreate = {
        name: form.value.name
      };

      this.contractTypeOneService.createContractTypeOne(contractTypeOneCreate).subscribe({
        next: response => {
          this.toastr.success('Typ 1 został dodany pomyślnie');
          form.reset();
          console.log(response);
        },
        error: error => {
          this.toastr.error(error.error);
          console.log('Błąd podczas dodawania typu 1: ', error);
        }
      });
    }
  }
}
