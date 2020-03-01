import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NoteListItem } from '../models/note-list-item';
import { SupplierService } from '../services/supplier.service';
import { NoteService } from '../services/note.service';
import { Supplier } from '../models/suplier';

@Component({
  selector: 'app-note-list',
  templateUrl: './note-list.component.html',
  styleUrls: ['./note-list.component.css']
})
export class NoteListComponent implements OnInit {

  supplierId: number;
  notes: Array<NoteListItem>;
  supplier: Supplier;
  // existed = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private supplierService: SupplierService,
    private noteService: NoteService
  ) { }

  ngOnInit() {
    this.route.params.subscribe(p => {
      this.supplierId = p['id'];
      this.supplierService.getNotes(this.supplierId).subscribe(s => this.notes = s);
      this.supplierService.getSupplier(this.supplierId).subscribe(s => this.supplier = s);
    })
  }

  onDelete(noteId: number) {
    var note = this.notes.find(n => n.id == noteId);
    this.noteService.setNoteStatus(noteId, true).subscribe(c => {
      note.isDeleted = true; 
      this.ngOnInit();
    });
  }

  onUndelete(noteId: number) {
    var note = this.notes.find(n => n.id == noteId);
    this.noteService.setNoteStatus(noteId, false).subscribe(c => {
      note.isDeleted = false;
      this.ngOnInit();
    });
  }

  onPurge(noteId: number) {
    this.noteService.deleteNote(noteId).subscribe(s => this.ngOnInit());
  }
}
