import { Component, OnInit } from '@angular/core';
import { ContractsService } from '../_services/contracts.service';
import { NgForm } from '@angular/forms';
import { catchError } from 'rxjs';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.css']
})
export class AddComponent implements OnInit {

  constructor(private contractService: ContractsService, private toastr: ToastrService) { }

  ngOnInit(): void {
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
        pdf: form.value.pdf
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
          console.error('Błąd podczas dodawania umowy:', error);
        }
      });
    }
  }
}