import { Component, OnInit } from '@angular/core';
import { ContractsService } from '../_services/contracts.service';
import { NgForm } from '@angular/forms';
import { catchError } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { ContractTypeOneService } from '../_services/contract-type-one.service';
import { ContractTypeOne } from '../_models/contractTypeOne';
import { ContractTypeTwoService } from '../_services/contract-type-two.service';
import { ContractTypeTwo } from '../_models/contractTypeTwo';


@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class AddComponent implements OnInit {
  contractTypeOne: ContractTypeOne[] = [];
  contractTypeTwo: ContractTypeTwo[] = [];

  constructor(private contractService: ContractsService, private contractTypeOneService: ContractTypeOneService, 
    private contractTypeTwoService: ContractTypeTwoService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.loadContractTypeOne();
    this.loadContractTypeTwo();
  }
  
  loadContractTypeOne() {
    this.contractTypeOneService.getContractTypeOnes().subscribe({
      next: (types: ContractTypeOne[]) => {
        this.contractTypeOne = types;
      },
      error: (error) => {
        console.error("Błąd podczas pobierania typów kontraktów: ", error);
      }
    });
  }

  loadContractTypeTwo() {
    this.contractTypeTwoService.GetContractTypeTwos().subscribe({
      next: (types: ContractTypeTwo[]) => {
        this.contractTypeTwo = types;
      },
      error: (error) => {
        console.error("Błąd podczas pobierania typów kontraktów: ", error);
      }
    });
  }

  onSubmit(form: NgForm) {
    if (form.valid) {
      const contractData = {
        contractNumber: form.value.contractNumber,
        name: form.value.name,
        description: form.value.description,
        dateOfConclusion: new Date(form.value.dateOfConclusion).toISOString(),
        value: form.value.value,
        contractor: form.value.contractor,
        signatory: form.value.signatory,
        hasPdf: form.value.hasPdf,
      };

      const contractTypeOneId = form.value.contractTypeOne;
      const contractTypeTwoId = form.value.contractTypeTwo;


      this.contractService.createContract(contractData, contractTypeOneId, contractTypeTwoId).subscribe({
        next: response => {
          this.toastr.success('Umowa została dodana pomyślnie');
          form.reset();
          console.log(response);
        },
        error: error => {
          this.toastr.error(error.error);
          console.error('Błąd podczas dodawania umowy: ', error);
        }
      });
    }
  }
}