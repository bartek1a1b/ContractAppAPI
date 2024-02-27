import { Component, OnInit } from '@angular/core';
import { ContractTypeTwo } from 'src/app/_models/contractTypeTwo';
import { ContractTypeTwoService } from 'src/app/_services/contract-type-two.service';

@Component({
  selector: 'app-contract-type-two-list',
  templateUrl: './contract-type-two-list.component.html',
  styleUrls: ['./contract-type-two-list.component.css']
})
export class ContractTypeTwoListComponent implements OnInit {
  contractTypeTwos: ContractTypeTwo[] = [];

  constructor(private contractTypeTwoService: ContractTypeTwoService) {}

  ngOnInit(): void {
    this.loadContractTypeTwos();
  }

  loadContractTypeTwos() {
    this.contractTypeTwoService.GetContractTypeTwos().subscribe({
      next: contractTypeTwos => this.contractTypeTwos = contractTypeTwos
    })
  }
}
