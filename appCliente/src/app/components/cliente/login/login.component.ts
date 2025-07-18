import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http'; // Para tipar o erro
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  usuario = { userName: '', password: '' };
  erroAutenticacao: boolean = false; 

  constructor(private authService: AuthService, private router: Router) {}

  onSubmit() {
    this.authService.login(this.usuario).subscribe(
      (response) => {
        console.log('Login bem-sucedido:', response);
        localStorage.setItem('token', response.jwtToken);
        this.router.navigate(['/pesquisa-cliente']);
      },
      (error: HttpErrorResponse) => {
        console.error('Erro no login:', error);
        this.erroAutenticacao = true;
      }
    );
  }
}
