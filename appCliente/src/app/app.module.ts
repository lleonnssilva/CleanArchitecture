import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http'; // Importando o HttpClientModule
import { AppRoutingModule } from './app-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CadastroClienteComponent } from './components/cliente/cadastro/cadastro-cliente.component';
import { AtualizarClienteComponent } from './components/cliente/atualizar/atualizar-cliente.component';
import { PesquisaClienteComponent } from './components/cliente/pesquisa/pesquisa-cliente.component';
import { AuthInterceptor } from './inteceptors/auth.interceptor';
import { HomeComponent } from './components/home/home.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    CadastroClienteComponent,
    PesquisaClienteComponent,
    AtualizarClienteComponent,
    HomeComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule ,
    HttpClientModule,
    ReactiveFormsModule
  ],
   providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor, 
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
