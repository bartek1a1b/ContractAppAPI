import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AnnexService } from 'src/app/_services/annex.service';

@Component({
  selector: 'app-annex-add',
  templateUrl: './annex-add.component.html',
  styleUrls: ['./annex-add.component.css']
})
export class AnnexAddComponent implements OnInit {

  constructor(private annexService: AnnexService, private toastr: ToastrService) {}

  ngOnInit(): void {
    
  }

  onSubmit(form: NgForm) {
    
    if (form.valid) {
      const dateOfConclusion = form.value.dateOfConclusion;

      if (!this.annexService.validateDate(dateOfConclusion)) {
        this.toastr.error('Wprowadź poprawną datę.');
        return;
      }

      const annexToTheContractCreate = {
        contractId: form.value.contractId,
        annexNumber: form.value.annexNumber,
        name: form.value.name,
        description: form.value.description,
        dateOfConclusion: new Date(form.value.dateOfConclusion).toISOString(),
        contractor: form.value.contractor,
        signatory: form.value.signatory,
        hasPdf: form.value.hasPdf
      };

      const contractId = form.value.contractId;

      this.annexService.createAnnexToTheContract(annexToTheContractCreate, contractId).subscribe({
        next: response => {
          this.toastr.success('Aneks został dodany pomyślnie');
          form.reset();
          console.log(response);
        },
        error: error => {
          this.toastr.error(error.error);
          console.error('Błąd podczas dodawania aneksu: ', error);
        }
      });
    }
  }

}
