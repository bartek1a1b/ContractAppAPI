import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Contract } from 'src/app/_models/contract';
import { ContractsService } from 'src/app/_services/contracts.service';

@Component({
  selector: 'app-contract-edit',
  templateUrl: './contract-edit.component.html',
  styleUrls: ['./contract-edit.component.css']
})
export class ContractEditComponent implements OnInit {
  conId: number | undefined;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private contractService: ContractsService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.conId = +params['id'];
    });
  }

  onSubmit(form: NgForm) {
    if (form.valid && this.conId !== undefined) {
      const updatedContract = {
        id: this.conId,
        contractNumber: form.value.contractNumber,
        name: form.value.name,
        description: form.value.description,
        dateOfConclusion: new Date(form.value.dateOfConclusion).toISOString(),
        value: form.value.value,
        contractor: form.value.contractor,
        signatory: form.value.signatory,
        pdf: form.value.pdf,
        typeNameOne: form.value.typeNameOne,
        typeNameTwo: form.value.typeNameTwo, 
        contractTypeOne: form.value.contractTypeOne,
        contractTypeTwo: form.value.contractTypeTwo
      };

      const contractTypeOneId = form.value.contractTypeOne;
      const contractTypeTwoId = form.value.contractTypeTwo;


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
  
}
