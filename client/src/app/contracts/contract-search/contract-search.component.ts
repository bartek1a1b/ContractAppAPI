import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Contract } from 'src/app/_models/contract';
import { Pagination } from 'src/app/_models/pagination';
import { ContractsService } from 'src/app/_services/contracts.service';

@Component({
  selector: 'app-contract-search',
  templateUrl: './contract-search.component.html',
  styleUrls: ['./contract-search.component.css']
})
export class ContractSearchComponent implements OnInit {
  contracts: Contract[] = [];
  pagination: Pagination | undefined;
  pageNumber = 1;
  pageSize = 100;
  searchPhrase: string = '';

  constructor(private contractsService: ContractsService, private route: ActivatedRoute) {}

  ngOnInit(): void {
    
  }

  

  
}
