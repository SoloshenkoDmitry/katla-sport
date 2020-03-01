import { Component, OnInit } from '@angular/core';
import { SupplierListItem } from '../models/supplier-list-item';
import { SupplierService } from '../services/supplier.service';

@Component({
    selector: 'app-supplier-list',
    templateUrl: './supplier-list.component.html',
    styleUrls: ['./supplier-list.component.css']
})
export class SupplierListComponent implements OnInit {

    suppliers: SupplierListItem[];
  
    constructor(private supplierService: SupplierService) { }
  
    ngOnInit() {
        this.getSuppliers();
    }

    getSuppliers() {
        this.supplierService.getSuppliers().subscribe(s => this.suppliers = s);
    }

    onDelete(supplierId: number) {
        var supplier = this.suppliers.find(s => s.id == supplierId);
        this.supplierService.setSupplierStatus(supplierId, true).subscribe(c => supplier.isDeleted = true);
    }
    
      onRestore(supplierId: number) {
        var supplier = this.suppliers.find(s => s.id == supplierId);
        this.supplierService.setSupplierStatus(supplierId, false).subscribe(c => supplier.isDeleted = false);
    }
}  