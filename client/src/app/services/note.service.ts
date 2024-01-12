import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Note } from '../interfaces/interfaces';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class NoteService {
  notes: Note[] = []
  constructor(private http: HttpClient) {}

  Create(note: Note): Observable<string> {
    return this.http.post<string>('https://localhost:7210/api/Notes/', note);
  }
  Get(): Observable<Note[]> {
    return this.http.get<Note[]>('https://localhost:7210/api/Notes/');
  }
  GetById(id: any):Note{
    console.log('В GetById' + this.notes[2]);
    return this.notes[id];
  }
}
