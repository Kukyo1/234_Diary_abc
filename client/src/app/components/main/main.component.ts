import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Note } from 'src/app/interfaces/interfaces';
import { NoteService } from 'src/app/services/note.service';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css'],
  encapsulation: ViewEncapsulation.None,
})
export class MainComponent implements OnInit {
  constructor(private note: NoteService) {}

  notes: Note[] = [];
  selectedIndex?: Number;
  title?: string;
  body?: string;

  ngOnInit(): void {
    this.note.Get().subscribe({
      next: (res)=>{
        this.notes=res;
        this.note.notes = this.notes;
      }
    })
  }
  public onClick(i: number) {
    this.selectedIndex = i;
    this.title = this.notes[i].title;
    this.body = this.notes[i].body;
  }
}
