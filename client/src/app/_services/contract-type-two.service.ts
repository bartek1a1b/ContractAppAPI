import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ContractTypeTwo } from '../_models/contractTypeTwo';

@Injectable({
  providedIn: 'root'
})
export class ContractTypeTwoService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  GetContractTypeTwos() {
    return this.http.get<ContractTypeTwo[]>(this.baseUrl + 'contractTypeTwo');
  }

  CreateContractTypeTwo(contractTypeTwoCreate: any) {
    const url = `${this.baseUrl}ContractTypeTwo`;
    return this.http.post(url, contractTypeTwoCreate, {responseType: 'text'});
  }
}
