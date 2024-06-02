import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ContractTypeOne } from '../_models/contractTypeOne';
import { ContractTypeTwo } from '../_models/contractTypeTwo';
import { Observable } from 'rxjs';
import { Contract } from '../_models/contract';

@Injectable({
  providedIn: 'root'
})
export class ContractTypeOneService {
  baseUrl = environment.apiUrl;
  

  constructor(private http: HttpClient) { }

  getContractTypeOnes() {
    return this.http.get<ContractTypeOne[]>(this.baseUrl + 'contractTypeOne');
  }

  getTypeTwoByTypeOne(contractTypeOneId: number) {
    const url = `${this.baseUrl}contractTypeOne/${contractTypeOneId}/contractTypeTwos`;
    return this.http.get<ContractTypeTwo[]>(url);
  }

  getContractsByTypeOne(contractTypeOneId: number): Observable<Contract[]> {
    return this.http.get<Contract[]>(`${this.baseUrl}ContractTypeOne/contractsOne/${contractTypeOneId}`);
  }

  createContractTypeOne(contractTypeOneCreate: any) {
    const url = `${this.baseUrl}ContractTypeOne`;
    return this.http.post(url, contractTypeOneCreate, {responseType: 'text'});
  }

  deleteContractTypeOne(contractTypeOneId: number) {
    return this.http.delete(this.baseUrl + 'contractTypeOne/delete-contractTypeOne/' + contractTypeOneId);
  }
}
