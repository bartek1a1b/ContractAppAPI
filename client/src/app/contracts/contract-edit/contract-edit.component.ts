import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Contract } from 'src/app/_models/contract';
import { ContractTypeOne } from 'src/app/_models/contractTypeOne';
import { ContractTypeTwo } from 'src/app/_models/contractTypeTwo';
import { ContractTypeOneService } from 'src/app/_services/contract-type-one.service';
import { ContractTypeTwoService } from 'src/app/_services/contract-type-two.service';
import { ContractsService } from 'src/app/_services/contracts.service';

@Component({
  selector: 'app-contract-edit',
  templateUrl: './contract-edit.component.html',
  styleUrls: ['./contract-edit.component.css']
})
export class ContractEditComponent implements OnInit {
  conId: number | undefined;
  contract: Contract | undefined;
  contractForm: FormGroup;
  contractTypeOne: ContractTypeOne[] = [];
  contractTypeTwo: ContractTypeTwo[] = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private contractService: ContractsService,
    private contractTypeOneService: ContractTypeOneService,
    private contractTypeTwoService: ContractTypeTwoService,
    private fb: FormBuilder,
    private toastr: ToastrService
  ) { 
    this.contractForm = this.fb.group({
      contractNumber: [''],
      name: [''],
      description: [''],
      typeNameOne: [''],
      typeNameTwo: [''],
      contractTypeOne: [''],
      contractTypeTwo: [''],
      dateOfConclusion: [''],
      value: [''],
      contractor: [''],
      signatory: ['']
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.conId = +params['id'];
      this.loadContractDetails();
    });
    this.loadContractTypeOne();
    this.loadContractTypeTwo();
  }

  loadContractDetails() {
    if (this.conId !== undefined) {
      this.contractService.getContract(this.conId.toString()).subscribe({
        next: (contract: Contract) => {
          this.contract = contract;
          this.populateForm(contract);
          if (contract.contractPdfs && contract.contractPdfs.length > 0) {
            // Jeśli tak, ustaw hasPdf na true w formularzu
            this.contractForm.patchValue({ hasPdf: true });
        }
        },
        error: error => {
          this.toastr.error('Błąd podczas pobierania danych kontraktu');
          console.error('Błąd podczas pobierania danych kontraktu:', error);
        }
      });
    }
  }

  populateForm(contract: Contract) {
    this.contractForm.patchValue({
      contractNumber: contract.contractNumber,
      name: contract.name,
      description: contract.description,
      typeNameOne: contract.typeNameOne,
      typeNameTwo: contract.typeNameTwo,
      contractTypeOne: contract.contractTypeOne.id,
      contractTypeTwo: contract.contractTypeTwo.id,
      dateOfConclusion: new Date(contract.dateOfConclusion).toISOString().slice(0, 10),
      value: contract.value,
      contractor: contract.contractor,
      signatory: contract.signatory,
      hasPdf: contract.hasPdf = true
    });
  }

  // onSubmit(form: NgForm) {
    onSubmit() {
    if (this.contractForm.valid && this.conId !== undefined) {
    // if (form.valid && this.conId !== undefined) {
      const updatedContract: Contract = {
        id: this.conId,
        contractNumber: this.contractForm.value.contractNumber,
        name: this.contractForm.value.name,
        description: this.contractForm.value.description,
        dateOfConclusion: new Date(this.contractForm.value.dateOfConclusion).toISOString(),
        value: this.contractForm.value.value,
        contractor: this.contractForm.value.contractor,
        signatory: this.contractForm.value.signatory,
        hasPdf: this.contractForm.value.hasPdf,
        typeNameOne: this.contractForm.value.typeNameOne,
        typeNameTwo: this.contractForm.value.typeNameTwo, 
        contractTypeOne: this.contractForm.value.contractTypeOne,
        contractTypeTwo: this.contractForm.value.contractTypeTwo,
        contractPdfs: [this.contractForm.value.contractPdf],
        annexToTheContract: [this.contractForm.value.annexToTheContract]
      };

      const contractTypeOneId = this.contractForm.value.contractTypeOne;
      const contractTypeTwoId = this.contractForm.value.contractTypeTwo;


      this.contractService.updateContract(this.conId, contractTypeOneId, contractTypeTwoId, updatedContract).subscribe({
        next: response => {
          this.toastr.success('Umowa została zaktualizowana');
          console.log(response);
        },
        error: error => {
          this.toastr.error(error.error);
          console.error('Błąd podczas edytowania umowy:', error);
        }
      });
    }
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
  
}
