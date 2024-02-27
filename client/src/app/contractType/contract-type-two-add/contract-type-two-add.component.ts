import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ContractTypeTwoService } from 'src/app/_services/contract-type-two.service';

@Component({
  selector: 'app-contract-type-two-add',
  templateUrl: './contract-type-two-add.component.html',
  styleUrls: ['./contract-type-two-add.component.css']
})
export class ContractTypeTwoAddComponent implements OnInit {
  
  constructor(private contractTypeTwoService: ContractTypeTwoService, private toastr: ToastrService) {}
  ngOnInit(): void {

  }

  onSubmit(form: NgForm) {
    if (form.valid) {
      const contractTypeTwoCreate = {
        name: form.value.name
      };

      this.contractTypeTwoService.CreateContractTypeTwo(contractTypeTwoCreate).subscribe({
        next: response => {
          this.toastr.success('Typ 2 został dodany pomyślnie');
          form.reset();
          console.log(response);
        },
        error: error => {
          this.toastr.error(error.error);
          console.log('Błąd podczas dodawanie typu 2: ', error);
        }
      });
    }
  }
}
