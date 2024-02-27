import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ContractTypeOne } from '../_models/contractTypeOne';

@Injectable({
  providedIn: 'root'
})
export class ContractTypeOneService {
  baseUrl = environment.apiUrl;
  

  constructor(private http: HttpClient) { }

  getContractTypeOnes() {
    return this.http.get<ContractTypeOne[]>(this.baseUrl + 'contractTypeOne');
  }

  createContractTypeOne(contractTypeOneCreate: any) {
    const url = `${this.baseUrl}ContractTypeOne`;
    return this.http.post(url, contractTypeOneCreate, {responseType: 'text'});
  }

  deleteContractTypeOne(contractTypeOneId: number) {
    return this.http.delete(this.baseUrl + 'contractTypeOne/delete-contractTypeOne/' + contractTypeOneId);
  }
}
