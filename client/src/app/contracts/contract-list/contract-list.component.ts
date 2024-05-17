import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { AnnexToTheContract } from 'src/app/_models/annexToTheContract';
import { Contract } from 'src/app/_models/contract';
import { Pagination } from 'src/app/_models/pagination';
import { ContractsService } from 'src/app/_services/contracts.service';

@Component({
  selector: 'app-contract-list',
  templateUrl: './contract-list.component.html',
  styleUrls: ['./contract-list.component.css']
})
export class ContractListComponent implements OnInit {
  @Input() contract: Contract | undefined;
  @ViewChild('pdfInput') pdfInput: any;
  contracts: Contract[] = [];
  annexToTheContract: AnnexToTheContract[] = [];
  pagination: Pagination | undefined;
  pageNumber = 1;
  pageSize = 10;
  searchPhrase = '';

  constructor(private contractService: ContractsService, private fb: FormBuilder, private router: Router) { }

  ngOnInit(): void {
    this.loadContracts();
  }

  // loadContracts() {
  //   this.contractService.getContracts(this.pageNumber, this.pageSize).subscribe({
  //     next: response => {
  //       if (response.result && response.pagination) {
  //         this.contracts = response.result;
  //         this.pagination = response.pagination;
  //       }
  //     }
  //   })
  // }

  loadContracts() {
    this.contractService.getContracts(this.pageNumber, this.pageSize).subscribe({
      next: response => {
        if (response.result && response.pagination) {
          this.contracts = response.result;
          this.pagination = response.pagination;
  
          this.contracts.forEach(contract => {
            if (contract.contractPdfs) { // Sprawdź, czy contractPdfs istnieje
              contract.hasPdf = contract.contractPdfs.length > 0; // Sprawdź, czy istnieją pliki PDF
            }
          });
        }
      }
    })
  }

  loadAnnexes(contractId: number) {
    this.contractService.getAnnexByContract(contractId).subscribe({
      next: annexes => {
        // Przypisz pobrane aneksy do odpowiedniego kontraktu na podstawie jego ID
        const contract = this.contracts.find(c => c.id === contractId);
        if (contract) {
          contract.annexToTheContract = annexes;
        }
      }
    });
  }

  deleteContract(conId: number) {
    this.contractService.deleteContract(conId).subscribe({
      next: _ => {
        if (this.contract) {
          this.contracts = this.contracts.filter(c => c.id !== conId);
        }
      },
      complete: () => {
        this.loadContracts();
      }
    });
  }

  onFileSelected(event: any, contractId: number): void {
    const file: File = event.target.files[0];
    if (file) {
        this.contractService.uploadPdf(file, contractId).subscribe(() => {
            // Aktualizuj listę umów po dodaniu pliku PDF
            this.loadContracts();
        });
    }
  }

  downloadPdf(contractId: number): void {
    this.contractService.downloadContractPdf(contractId).subscribe(response => {
      const blob = new Blob([response], { type: 'application/pdf' });
      const url = window.URL.createObjectURL(blob);
      const anchor = document.createElement('a');
      anchor.href = url;
      anchor.download = `kontrakt_${contractId}.pdf`;
      anchor.click();
      window.URL.revokeObjectURL(url);
    });
  }

  pageChanged(event: any) {
    if (this.pageNumber !== event.page) {
      this.pageNumber = event.page;
      this.loadContracts();
    }
  }

  fetchData(): void {
    this.contractService.getSearchContracts(this.searchPhrase).subscribe(contracts => {
      this.contracts = contracts;
      console.log(this.contracts);
    })
  }

  searchForm = this.fb.nonNullable.group({
    searchPhrase: '',
  })

  onSearchSubmit(): void {
    this.searchPhrase= this.searchForm.value.searchPhrase ?? '';
    this.fetchData();
  }

  clearSearch(): void {
    this.loadContracts();
    this.searchForm.reset();
  }

}
