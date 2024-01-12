import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { Note } from 'src/app/interfaces/interfaces';
import { NoteService } from 'src/app/services/note.service';

@Component({
  selector: 'app-edit',
  templateUrl: './edit.component.html',
  styleUrls: ['./edit.component.css']
})
export class EditComponent implements OnInit {

  CurrentNote?: Note
  htmlContent: string = "123123"
  editorConfig: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    sanitize: false,
    height: '20vh',
    minHeight: '0',
    maxHeight: 'auto',
    width: 'auto',
    minWidth: '0',
    translate: 'yes',
    enableToolbar: true,
    showToolbar: true,
    placeholder: 'This is body',
    defaultParagraphSeparator: '',
    defaultFontName: 'Rubik',
    defaultFontSize: '',
    fonts: [
      { class: 'arial', name: 'Arial' },
      { class: 'times-new-roman', name: 'Times New Roman' },
      { class: 'calibri', name: 'Calibri' },
      { class: 'comic-sans-ms', name: 'Comic Sans MS' },
      { class: 'rubik', name: 'Rubik' },
    ],
    toolbarPosition: 'top',
    toolbarHiddenButtons: [['insertImage', 'insertVideo', 'toggleEditorMode']],
  };


  constructor(private aroute: ActivatedRoute, private note: NoteService){
    console.log('In constructor')
    this.aroute.params.subscribe(params => {
      if(params['id']){
        console.log('Id: '+ params['id']);
        this.CurrentNote = this.note.GetById(params['id']);
        console.log(this.CurrentNote.body);
        this.htmlContent = this.CurrentNote.body;
      }
    })
  }
  ngOnInit(): void {
    // this.CurrentNote = this.note.GetById(this.aroute.snapshot.paramMap.get('id'));
    // this.htmlContent = this.CurrentNote.body;
    console.log('InOnInit');
  }
  noteForm: FormGroup = new FormGroup({
    title: new FormControl('', Validators.required),
    body: new FormControl('', Validators.required),
  });
  Edit(){}
  initTemplate(){
    console.log('In template');
  }
}
