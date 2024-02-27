import { Component, Input, OnInit } from '@angular/core';
import { ContractTypeOne } from 'src/app/_models/contractTypeOne';
import { ContractTypeOneService } from 'src/app/_services/contract-type-one.service';

@Component({
  selector: 'app-contract-type-one-list',
  templateUrl: './contract-type-one-list.component.html',
  styleUrls: ['./contract-type-one-list.component.css']
})
export class ContractTypeOneListComponent implements OnInit {
  @Input() contractTypeOne: ContractTypeOne | undefined;
  contractTypeOnes: ContractTypeOne[] = [];

  constructor(private contractTypeOneService: ContractTypeOneService) {}

  ngOnInit(): void {
    this.loadContractTypeOnes();
  }

  loadContractTypeOnes() {
    this.contractTypeOneService.getContractTypeOnes().subscribe({
      next: contractTypeOnes => this.contractTypeOnes = contractTypeOnes
    })
  }

  deleteContractTypeOne(contractTypeOneId: number) {
    this.contractTypeOneService.deleteContractTypeOne(contractTypeOneId).subscribe({
      next: _ => {
        if (this.contractTypeOne) {
          this.contractTypeOnes = this.contractTypeOnes.filter(ct => ct.id !== contractTypeOneId);
        }
      },
      complete: () => {
        this.loadContractTypeOnes();
      }
    });
  }
}
