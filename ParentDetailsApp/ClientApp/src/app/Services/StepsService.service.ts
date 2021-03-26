import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Step } from '../Models/step';

const baseUrl = 'http://localhost:5001/api/step';

@Injectable({
  providedIn: 'root'
})
export class StepsService {

   httpOptions = {
   headers: new HttpHeaders({'Content-Type':  'application/json' })};

  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get<Step[]>(baseUrl);
  }

  get(id) {
    return this.http.get<Step>(`${baseUrl}/${id}`);
  }

  create(data) {
    return this.http.post(baseUrl, data, this.httpOptions);
  }

  update(id, data: Step) {
    return this.http.put(`${baseUrl}/${id}`, data);
  }

  delete(id) {
    return this.http.delete(`${baseUrl}/${id}`);
  }

  deleteAll() {
    return this.http.delete(baseUrl);
  }

  findByTitle(title) {
    return this.http.get(`${baseUrl}?title=${title}`);
  }
}
