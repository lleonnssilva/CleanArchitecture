import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './guards/auth.guard'; // Importando o guard
import { LoginComponent } from './components/cliente/login/login.component';
import { CadastroComponent } from './components/cliente/cadastro/cadastro-cliente.component';
import { PesquisaClienteComponent } from './components/cliente/pesquisa/pesquisa-cliente.component';
import { AtualizarClienteComponent } from './components/cliente/atualizar/atualizar-cliente.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'cadastro', component: CadastroComponent },
  { path: 'pesquisa-cliente', component: PesquisaClienteComponent,canActivate: [AuthGuard]}, // Protegendo com Guard, 
  { path: 'atualizar-cliente/:id', component: AtualizarClienteComponent,canActivate: [AuthGuard] }, // Protegendo com Guard
  { path: '', redirectTo: '/login', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
