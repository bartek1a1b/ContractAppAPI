import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { ContractTypeTwo } from '../_models/contractTypeTwo';
import { Observable } from 'rxjs';
import { Contract } from '../_models/contract';

@Injectable({
  providedIn: 'root'
})
export class ContractTypeTwoService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  GetContractTypeTwos() {
    return this.http.get<ContractTypeTwo[]>(this.baseUrl + 'contractTypeTwo');
  }

  getContractsByTypeTwo(contractTypeTwoId: number): Observable<Contract[]> {
    return this.http.get<Contract[]>(`${this.baseUrl}ContractTypeTwo/contractsTwo/${contractTypeTwoId}`);
  }

  createContractTypeTwo(contractTypeOneId: number, contractTypeTwoCreate: any): Observable<string> {
    const url = `${this.baseUrl}ContractTypeTwo?contractTypeOneId=${contractTypeOneId}`;
    return this.http.post(url, contractTypeTwoCreate, {responseType: 'text'});
  }

  // CreateContractTypeTwo(contractTypeTwoCreate: any) {
  //   const url = `${this.baseUrl}ContractTypeTwo`;
  //   return this.http.post(url, contractTypeTwoCreate, {responseType: 'text'});
  // }
}
