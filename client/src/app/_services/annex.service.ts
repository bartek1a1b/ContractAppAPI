import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { AnnexToTheContract } from '../_models/annexToTheContract';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AnnexService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getAnnexesToTheContract() {
    return this.http.get<AnnexToTheContract[]>(this.baseUrl + 'AnnexToTheContract');
  }

  createAnnexToTheContract(annexToTheContractCreate: any, contractId: number) {
    const url = `${this.baseUrl}AnnexToTheContract?contractId=${contractId}`;
    return this.http.post(url, annexToTheContractCreate, {responseType: 'text'});
  }

  deleteAnnexToTheContract(annexToTheContractId: number) {
    return this.http.delete(this.baseUrl + 'AnnexToTheContract/delete-annexToTheContract/' + annexToTheContractId);
  }

  downloadPdf(annextId: number): Observable<any> {
    return this.http.get(`${this.baseUrl}Pdf/get-pdfByAnnexId?annexId=${annextId}`, { responseType: 'blob' });
  }

  uploadPdf(file: File, annexId: number): Observable<any> {
    const formData: FormData = new FormData();
    formData.append('file', file, file.name);
    formData.append('annexToTheContractId', annexId.toString());
    return this.http.post(`${this.baseUrl}Pdf`, formData);
  }

  validateDate(date: string): boolean {
    const currentDate = new Date();
    const selectedDate = new Date(date);
    return selectedDate <= currentDate;
  }
}
