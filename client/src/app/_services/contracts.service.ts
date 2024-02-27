import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Contract } from '../_models/contract';
import { Observable, catchError, map, throwError } from 'rxjs';
import { PaginatedResult } from '../_models/pagination';

@Injectable({
  providedIn: 'root'
})
export class ContractsService {
  baseUrl = environment.apiUrl;
  paginatedResult: PaginatedResult<Contract[]> = new PaginatedResult<Contract[]>;

  constructor(private http: HttpClient) { }

  getContracts(page?: number, itemsPerPage?: number) {
    let params = new HttpParams();

    if (page && itemsPerPage) {
      params = params.append('pageNumber', page);
      params = params.append('pageSize', itemsPerPage);
    }

    return this.http.get<Contract[]>(this.baseUrl + 'contract/dtos', {observe: 'response', params}).pipe(
      map(response => {
        if (response.body) {
          this.paginatedResult.result = response.body;
        }
        const pagination = response.headers.get('Pagination');
        if (pagination) {
          this.paginatedResult.pagination = JSON.parse(pagination);
        }
        return this.paginatedResult;
      })
    )
  }

  getContract(id: string): Observable<Contract> {
    return this.http.get<Contract>(this.baseUrl + 'contract/' + id);
  }

  createContract(contractData: any, contractTypeOneId: number, contractTypeTwoId: number) {
    const url = `${this.baseUrl}contract?contractTypeOneId=${contractTypeOneId}&contractTypeTwoId=${contractTypeTwoId}`;
    return this.http.post(url, contractData, {responseType: 'text'});
  }

  getSearchContracts(searchPhrase: string): Observable<Contract[]> {
    const url = `${this.baseUrl}contract/search/?searchPhrase=${searchPhrase}`;
    return this.http.get<Contract[]>(url);
  }

}
