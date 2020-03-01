import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MainPageComponent } from 'app/main-page/main-page.component';
import { HiveFormComponent } from './hive-management/forms/hive-form.component';
import { HiveSectionFormComponent } from './hive-management/forms/hive-section-form.component';
import { HiveListComponent } from './hive-management/lists/hive-list.component';
import { HiveSectionListComponent } from './hive-management/lists/hive-section-list.component';
import { ProductCategoryFormComponent } from './product-management/forms/product-category-form.component';
import { ProductFormComponent } from './product-management/forms/product-form.component';
import { ProductCategoryListComponent } from './product-management/lists/product-category-list.component';
import { ProductCategoryProductListComponent } from './product-management/lists/product-category-product-list.component';
import { ProductListComponent } from './product-management/lists/product-list.component';

import { SupplierListComponent } from './supplier-management/lists/supplier-list.component';
import { SupplierFormComponent } from './supplier-management/forms/supplier-form.component';
import { NoteListComponent } from './supplier-management/lists/note-list.component';
import { NoteFormComponent } from './supplier-management/forms/note-form.component';


const routes: Routes = [
  { path: '', redirectTo: '/main', pathMatch: 'full' },
  { path: 'main', component: MainPageComponent },
  { path: 'categories', component: ProductCategoryListComponent },
  { path: 'category', component: ProductCategoryFormComponent },
  { path: 'category/:id', component: ProductCategoryFormComponent },
  { path: 'category/:id/products', component: ProductCategoryProductListComponent },
  { path: 'products', component: ProductListComponent },
  { path: 'product/:id', component: ProductFormComponent },
  { path: 'category/:categoryId/product/:id', component: ProductFormComponent },
  { path: 'hives', component: HiveListComponent },
  { path: 'hive', component: HiveFormComponent },
  { path: 'hive/:id', component: HiveFormComponent },
  { path: 'hive/:id/sections', component: HiveSectionListComponent },
  { path: 'section/:id', component: HiveSectionFormComponent },
  { path: 'hive/:hiveId/section/:id', component: HiveSectionFormComponent },
  { path: 'hive/:hiveId/section', component: HiveSectionFormComponent },

  { path: 'suppliers', component: SupplierListComponent },
  { path: 'supplier', component: SupplierFormComponent },
  { path: 'supplier/:id', component: SupplierFormComponent },
  { path: 'supplier/:id/notes', component: NoteListComponent },
  { path: 'note/:noteId', component: NoteFormComponent },
  { path: 'supplier/:supplierId/note/:parentId', component: NoteFormComponent },
  { path: 'supplier/:supplierId/note', component: NoteFormComponent },
];

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ]
})
export class AppRoutingModule { }
