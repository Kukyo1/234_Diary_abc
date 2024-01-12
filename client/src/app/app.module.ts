import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { HomeComponent } from './components/home/home.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { MainComponent } from './components/main/main.component';
import { RouterModule } from '@angular/router';
import { CreateComponent } from './components/create/create.component';
import { AngularEditorModule } from '@kolkov/angular-editor';
import { TokenInterceptorProvider } from './interceptor.interceptor';
import { AuthGuardService } from './services/auth-guard.service';
import { HtmlStylePipe } from './html-style.pipe';
import { EditComponent } from './components/edit/edit.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent,
    MainComponent,
    CreateComponent,
    HtmlStylePipe,
    EditComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    RouterModule,
    AngularEditorModule,
    FormsModule
  ],
  providers: [TokenInterceptorProvider, AuthGuardService],
  bootstrap: [AppComponent],
})
export class AppModule {}
