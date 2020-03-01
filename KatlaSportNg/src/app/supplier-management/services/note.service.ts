import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import { Note } from '../models/note';
import { NoteListItem } from '../models/note-list-item';

@Injectable({
  providedIn: 'root'
})
export class NoteService {
  private url = environment.apiUrl + 'api/notes/';

  constructor(private http: HttpClient) { }

  getNote(noteId: number): Observable<Note> {
    return this.http.get<Note>(`${this.url}${noteId}`);
  }

  setNoteStatus(noteId: number, deletedStatus: boolean): Observable<Object> {
    return this.http.put(`${this.url}${noteId}/status/${deletedStatus}`, null);
  }

  addNote(note: Note): Observable<Note> {
    return this.http.post<Note>(`${this.url}`, note);
  }

  updateNote(note: Note): Observable<Note> {
    return this.http.put<Note>(`${this.url}${note.id}`, note);
  }

  deleteNote(noteId: number): Observable<Note> {
    return this.http.delete<Note>(`${this.url}${noteId}`);
  }
}
