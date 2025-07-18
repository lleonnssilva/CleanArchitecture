import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginResponse } from '../model/login.component';
import { environment } from '../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = `${environment.apiUrl}/auth/authenticate`;  // URL da API de login

  constructor(private http: HttpClient) {}

  // Método para fazer o login do usuário
  login(usuario: { userName: string, password: string }): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(this.apiUrl, usuario);  // Envia os dados de login para a API
  }

  // Método para fazer o logout do usuário
  logout(): void {
    localStorage.removeItem('token');  // Remove o token do localStorage
  }

  // Método para verificar se o usuário está autenticado
  isAuthenticated(): boolean {
    const token = localStorage.getItem('token');  // Verifica se existe um token no localStorage
    return token ? true : false;  // Retorna true se houver token, caso contrário, false
  }

  // Método para obter o token
  getToken(): string | null {
    return localStorage.getItem('token');  // Retorna o token armazenado ou null
  }
}
