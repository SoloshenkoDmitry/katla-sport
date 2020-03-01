import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NoteService } from '../services/note.service';
import { SupplierService } from '../services/supplier.service';
import { Note } from '../models/note';

@Component({
  selector: 'app-note-form',
  templateUrl: './note-form.component.html',
  styleUrls: ['./note-form.component.css']
})
export class NoteFormComponent implements OnInit {

  supplierId: number;
  parentId = null;

  note = new Note(0, 0, "", false, 0, "");
  existed = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private noteService: NoteService,
    private supplierService: SupplierService
  ) { }

  ngOnInit() {    
    this.route.params.subscribe(p =>{
      if(p['supplierId'] === undefined){
        if(p['noteId'] === undefined) return;
        this.noteService.getNote(p['noteId']).subscribe(ns => {
          this.note = ns;
          this.supplierId = this.note.supplierId;
        });
        this.existed = true;
        return;
      }

      if(p['parentId'] === undefined)  this.parentId = null;
      else this.parentId = p['parentId'];

      this.supplierId = p['supplierId'];            
    });
  }

  navigateToNote() {
    this.router.navigate([`/supplier/${this.supplierId}/notes`]);
  }

  onCancel() {
    this.navigateToNote();
  }

  onSubmit() {
    if (this.existed) {
      this.noteService.updateNote(this.note).subscribe(h => this.navigateToNote());
    } else {
      this.note.parentId = this.parentId;
      this.note.supplierId = this.supplierId;


      console.log("Text = " + this.note.supplierId);
      this.noteService.addNote(this.note).subscribe(h => this.navigateToNote());
    }
  }
}
