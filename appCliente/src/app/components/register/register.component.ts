import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http'; // Para tipar o erro
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  usuario = { 
    userName: '', 
    password: '', 
    email: '',    // Campo adicional para email
    nomeCompleto: '' // Campo adicional para nome completo (opcional)
  };
  erroCadastro: boolean = false; 

  constructor(private authService: AuthService, private router: Router) {}

  onSubmit() {
    // Aqui vamos chamar o serviço de registro, que seria diferente do login
    this.authService.register(this.usuario).subscribe(
      (response) => {
        console.log('Cadastro bem-sucedido:', response);
        localStorage.setItem('token', response.jwtToken);
        this.router.navigate(['/home']);  // Após o cadastro, redireciona para a tela de login
      },
      (error: HttpErrorResponse) => {
        console.error('Erro no cadastro:', error);
        this.erroCadastro = true;
      }
    );
  }
}
