import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { ContractsService } from '../_services/contracts.service';
import { NgForm } from '@angular/forms';
import { catchError, from } from 'rxjs';
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
  selectedContractTypeOneId: number | null = null;

  constructor(private contractService: ContractsService, private contractTypeOneService: ContractTypeOneService, 
    private contractTypeTwoService: ContractTypeTwoService, private toastr: ToastrService, private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.loadContractTypeOne();
    // this.loadContractTypeTwo();
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

  onContractTypeOneChange(event: any) {
    const selectedValue = event.target?.value;
    if (selectedValue) {
      this.selectedContractTypeOneId = Number(selectedValue);
      console.log('Selected Contract Type One ID:', this.selectedContractTypeOneId);
      this.contractTypeOneService.getTypeTwoByTypeOne(this.selectedContractTypeOneId).subscribe({
        next: (response: ContractTypeTwo[]) => {
          console.log('Received response:', response);
          this.contractTypeTwo = response; // Przypisanie odpowiedzi bezpośrednio
          console.log('Assigned contractTypeTwo:', this.contractTypeTwo);
        },
        error: (error) => {
          console.error('Błąd podczas pobierania podkategorii: ', error);
        }
      });
    }
  }

  // loadContractTypeTwo() {
  //   this.contractTypeTwoService.GetContractTypeTwos().subscribe({
  //     next: (types: ContractTypeTwo[]) => {
  //       this.contractTypeTwo = types;
  //     },
  //     error: (error) => {
  //       console.error("Błąd podczas pobierania typów kontraktów: ", error);
  //     }
  //   });
  // }

  onSubmit(form: NgForm) {
    if (form.valid) {
      const dateOfConclusion = form.value.dateOfConclusion;

      if (!this.contractService.validateDate(dateOfConclusion)) {
        this.toastr.error('Wprowadź poprawną datę.');
        return;
      }
      const contractData = {
        contractNumber: form.value.contractNumber,
        name: form.value.name,
        description: form.value.description,
        dateOfConclusion: new Date(form.value.dateOfConclusion).toISOString(),
        value: form.value.value,
        contractor: form.value.contractor,
        signatory: form.value.signatory,
        hasPdf: form.value.hasPdf,
        contractTypeOneId: form.value.contractTypeOne,
        contractTypeTwoId: form.value.contractTypeTwo
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
          if (error.status == 422) {
            this.toastr.error('Umowa o takim numerze już istnieje');
          } else {
            this.toastr.error(error.error);
          console.error('Błąd podczas dodawania umowy: ', error);
          }
        }
      });
    }
  }
}