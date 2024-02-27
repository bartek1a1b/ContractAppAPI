import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { Contract } from 'src/app/_models/contract';
import { Pagination } from 'src/app/_models/pagination';
import { ContractsService } from 'src/app/_services/contracts.service';

@Component({
  selector: 'app-contract-list',
  templateUrl: './contract-list.component.html',
  styleUrls: ['./contract-list.component.css']
})
export class ContractListComponent implements OnInit {
  contracts: Contract[] = [];
  pagination: Pagination | undefined;
  pageNumber = 1;
  pageSize = 10;
  searchPhrase = '';

  constructor(private contractService: ContractsService, private fb: FormBuilder, private router: Router) { }

  ngOnInit(): void {
    this.loadContracts();
  }

  loadContracts() {
    this.contractService.getContracts(this.pageNumber, this.pageSize).subscribe({
      next: response => {
        if (response.result && response.pagination) {
          this.contracts = response.result;
          this.pagination = response.pagination;
        }
      }
    })
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
  }

}
