import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SupplierService } from '../services/supplier.service';
import { Supplier } from '../models/suplier';

@Component({
    selector: 'app-supplier-form',
    templateUrl: './supplier-form.component.html',
    styleUrls: ['./supplier-form.component.css']
})
export class SupplierFormComponent implements OnInit {
  supplier = new Supplier(0, "", "", "", false, null, "");
  existed = false;

  selectedFileName = "Choose file";
  avatar: any = null;
  propagateChange = (_: any) => { };
  
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private supplierService: SupplierService
  ) { }

  ngOnInit() {
      this.route.params.subscribe(p => {
        if (p['id'] === undefined) return;
        this.supplierService.getSupplier(p['id']).subscribe(h => this.supplier = h);
        this.existed = true;
    });
  }

  navigateToSuppliers() {
     this.router.navigate(['/suppliers']);
  }

  onCancel() {
    this.navigateToSuppliers();
  }

  onSubmit() {
    if(this.existed){
      this.supplierService.updateSupplier(this.supplier).subscribe(h => this.navigateToSuppliers());
    }else{
      this.supplierService.addSupplier(this.supplier).subscribe(h => this.navigateToSuppliers());
    }
  }

  onDelete() {
    this.supplierService.setSupplierStatus(this.supplier.id, true).subscribe(h => this.supplier.isDeleted = true);
  }
  
  onUndelete() {
    this.supplierService.setSupplierStatus(this.supplier.id, false).subscribe(h => this.supplier.isDeleted = false);
  }
  
  onPurge() {
    this.supplierService.deleteSupplier(this.supplier.id).subscribe(h => this.navigateToSuppliers());
  }    

  addAvatar(event) {
    this.readThis(event.target);
  }  

  readThis(inputValue: any){
    var file: File = inputValue.files[0];
    var myReader: FileReader = new FileReader();

    this.selectedFileName = file.name;

    myReader.onloadend = (e) => {
      this.propagateChange(myReader.result);
      this.avatar = myReader.result;
      this.supplier.avatar = this.avatar;
    }

    myReader.readAsDataURL(file);    
  }
}