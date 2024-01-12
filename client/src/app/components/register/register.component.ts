import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  registerForm : FormGroup = new FormGroup({
    "name": new FormControl("", Validators.required),
    "email": new FormControl("", [Validators.required, Validators.email]),
    "password": new FormControl("", Validators.required)
  })

  error: string = ""
  constructor(private auth: AuthService, private router: Router ){}

  Register(){
    this.auth.Register(this.registerForm.value).subscribe({
      next: (res) =>{this.router.navigate(['/auth/login'])},
      error: (err) =>{this.error = err.error.message}
    })
  }
}
