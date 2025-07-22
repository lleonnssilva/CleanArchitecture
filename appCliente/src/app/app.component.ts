import { Component } from '@angular/core';
import { Router } from '@angular/router'; // O Router para navegação
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html', // O HTML do componente
  styleUrls: ['./app.component.css']   // O CSS do componente
})
export class AppComponent {
  title: string ="";
  constructor(public authService: AuthService, private router: Router) {}

  // Método para verificar se o usuário está logado
  isAuthenticated(): boolean {
    return this.authService.isAuthenticated();
  }

  // Método de logout
  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);  // Redireciona o usuário para o login após sair
  }
}
