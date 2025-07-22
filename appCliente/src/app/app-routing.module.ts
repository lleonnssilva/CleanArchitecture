import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './guards/auth.guard'; // Importando o guard
import { CadastroClienteComponent } from './components/cliente/cadastro/cadastro-cliente.component';
import { PesquisaClienteComponent } from './components/cliente/pesquisa/pesquisa-cliente.component';
import { AtualizarClienteComponent } from './components/cliente/atualizar/atualizar-cliente.component';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent },  // Home page route
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'cadastro', component: CadastroClienteComponent },
  { path: 'pesquisa-cliente', component: PesquisaClienteComponent,canActivate: [AuthGuard]}, // Protegendo com Guard, 
  { path: 'atualizar-cliente/:id', component: AtualizarClienteComponent,canActivate: [AuthGuard] }, // Protegendo com Guard
  { path: '', redirectTo: '/login', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
