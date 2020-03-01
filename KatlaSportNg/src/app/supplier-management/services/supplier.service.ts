import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { Supplier } from '../models/suplier';
import { SupplierListItem } from '../models/supplier-list-item';
import { NoteListItem } from '../models/note-list-item';

@Injectable({
    providedIn: 'root'
})
export class SupplierService {
    private url = environment.apiUrl + 'api/suppliers/';
  
    constructor(private http: HttpClient) { }

    getSuppliers(): Observable<Array<SupplierListItem>>{
        return this.http.get<Array<SupplierListItem>>(this.url);
    }

    getSupplier(supplierId: number): Observable<Supplier> {
        return this.http.get<Supplier>(`${this.url}${supplierId}`);
    }

    getNotes(supplierId: number): Observable<Array<NoteListItem>> {
        return this.http.get<Array<NoteListItem>>(`${this.url}${supplierId}/notes`);
    }

    addSupplier(supplier: Supplier): Observable<Supplier> {
        return this.http.post<Supplier>(`${this.url}`, supplier);
    }

    updateSupplier(supplier: Supplier): Observable<Supplier> {
        return this.http.put<Supplier>(`${this.url}${supplier.id}`, supplier);
    }

    setSupplierStatus(supplierId: number, deletedStatus: boolean): Observable<Object>{
        return this.http.put(`${this.url}${supplierId}/status/${deletedStatus}`, null);
    }

    deleteSupplier(supplierId: number): Observable<Object> {
        return this.http.delete(`${this.url}${supplierId}`);
    }
}  