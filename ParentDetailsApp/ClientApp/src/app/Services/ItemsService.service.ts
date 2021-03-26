import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Item } from '../Models/item';

const baseUrl = 'http://localhost:5001/api/item';

@Injectable({
  providedIn: 'root'
})
export class ItemsService {

  constructor(private http: HttpClient) { }

  getAll() {
    return this.http.get<Item[]>(baseUrl);
  }

  get(id) {
    return this.http.get<Item>(`${baseUrl}/${id}`);
  }

  create(data: Item) {
    return this.http.post(baseUrl, data);
  }

  update(id, data: Item) {
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
