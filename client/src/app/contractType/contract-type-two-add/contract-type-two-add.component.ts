import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { ContractTypeOne } from 'src/app/_models/contractTypeOne';
import { ContractTypeOneService } from 'src/app/_services/contract-type-one.service';
import { ContractTypeTwoService } from 'src/app/_services/contract-type-two.service';

@Component({
  selector: 'app-contract-type-two-add',
  templateUrl: './contract-type-two-add.component.html',
  styleUrls: ['./contract-type-two-add.component.css']
})
export class ContractTypeTwoAddComponent implements OnInit {
  contractTypeOnes: ContractTypeOne[] = [];
  
  constructor(private contractTypeTwoService: ContractTypeTwoService, private contractTypeOneService: ContractTypeOneService, private toastr: ToastrService) {}
  ngOnInit(): void {
    this.loadContractTypeOnes();
  }

  loadContractTypeOnes() {
    this.contractTypeOneService.getContractTypeOnes().subscribe({
      next: (response) => {
        this.contractTypeOnes = response;
      },
      error: (error) => {
        this.toastr.error('Błąd podczas pobierania kategorii')
      }
    });
  }

  onSubmit(form: NgForm) {
    if (form.valid) {
      const contractTypeTwoCreate = {
        name: form.value.name
      };

      const contractTypeOneId = form.value.contractTypeOneId;

      this.contractTypeTwoService.createContractTypeTwo(contractTypeOneId, contractTypeTwoCreate).subscribe({
        next: response => {
          this.toastr.success('Podkategoria została dodana pomyślnie');
          form.reset();
          console.log(response);
        },
        error: error => {
          this.toastr.error(error.error);
          console.log('Błąd podczas dodawania podkategorii: ', error);
        }
      });
    }
  }
}
