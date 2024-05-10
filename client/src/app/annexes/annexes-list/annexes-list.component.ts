import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AnnexToTheContract } from 'src/app/_models/annexToTheContract';
import { Contract } from 'src/app/_models/contract';
import { AnnexService } from 'src/app/_services/annex.service';
import { ContractsService } from 'src/app/_services/contracts.service';

@Component({
  selector: 'app-annexes-list',
  templateUrl: './annexes-list.component.html',
  styleUrls: ['./annexes-list.component.css']
})
export class AnnexesListComponent implements OnInit {
  @Input() annex: AnnexToTheContract | undefined;
  contract?: Contract;
  annexes: AnnexToTheContract[] = [];
  contractId: number | undefined;

  constructor(private annexService: AnnexService, private route: ActivatedRoute, private contractService: ContractsService, private router: Router) {}

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.contractId = +params['contractId'];
      if (this.contractId) {
        this.loadAnnexes(this.contractId);
      }
    });
  }

  loadAnnexesToTheContract() {
    this.annexService.getAnnexesToTheContract().subscribe({
      next: annexes => this.annexes = annexes
    })
  }

  loadAnnexes(contractId: number) {
    this.contractService.getAnnexByContract(contractId).subscribe({
      next: annexes => {
        this.annexes = annexes;
      },
      error: error => {
        console.error('Błąd podczas pobierania aneksów:', error);
      }
    });
  }

  downloadPdf(annexId: number): void {
    this.annexService.downloadPdf(annexId).subscribe(response => {
      const blob = new Blob([response], { type: 'application/pdf' });
      const url = window.URL.createObjectURL(blob);
      const anchor = document.createElement('a');
      anchor.href = url;
      anchor.download = `aneks_${annexId}.pdf`;
      anchor.click();
      window.URL.revokeObjectURL(url);
    });
  }

  deleteAnnexToTheContract(annexToTheContractId: number) {
    this.annexService.deleteAnnexToTheContract(annexToTheContractId).subscribe({
      next: _ => {
        if (this.annex) {
          this.annexes = this.annexes.filter(a => a.id !== annexToTheContractId);
        }
      },
      complete: () => {
        this.loadAnnexesToTheContract();
      }
    });
  }
}
