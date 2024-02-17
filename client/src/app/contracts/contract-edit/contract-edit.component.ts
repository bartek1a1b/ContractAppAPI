import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Contract } from 'src/app/_models/contract';
import { ContractsService } from 'src/app/_services/contracts.service';

@Component({
  selector: 'app-contract-edit',
  templateUrl: './contract-edit.component.html',
  styleUrls: ['./contract-edit.component.css']
})
export class ContractEditComponent implements OnInit {

  
  constructor(private contractService: ContractsService, private route: ActivatedRoute) {}

  ngOnInit(): void {
    
  }

  loadContract() {

  }


  updateContract() {
    
  }

}
