import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppComponent } from './app.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http'; // Importando o HttpClientModule
import { ClienteService } from './services/cliente.service';
import { AppRoutingModule } from './app-routing.module';
import { FormsModule } from '@angular/forms';
import { LoginComponent } from './components/cliente/login/login.component';
import { CadastroComponent } from './components/cliente/cadastro/cadastro-cliente.component';
import { AtualizarClienteComponent } from './components/cliente/atualizar/atualizar-cliente.component';
import { PesquisaClienteComponent } from './components/cliente/pesquisa/pesquisa-cliente.component';
import { AuthInterceptor } from './inteceptors/auth.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    CadastroComponent,
    PesquisaClienteComponent,
    AtualizarClienteComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule ,
    HttpClientModule
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
