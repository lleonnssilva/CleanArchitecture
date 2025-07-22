import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginResponse } from '../model/login.component';
import { environment } from '../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl = `${environment.apiUrl}/auth`;  // URL da API de login

  constructor(private http: HttpClient) {}

  // Método para fazer o login do usuário
  login(usuario: { userName: string, password: string }): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.apiUrl}/authenticate`, usuario);  // Envia os dados de login para a API
  }
  register(usuario: { userName: string, password: string }): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.apiUrl}/register`, usuario);  // Envia os dados de login para a API
  }
  // Método para fazer o logout do usuário
  logout(): void {
    localStorage.removeItem('token');  // Remove o token do localStorage
  }

  // Método para verificar se o usuário está autenticado
  isAuthenticated(): boolean {
    // const token = localStorage.getItem('token');  // Verifica se existe um token no localStorage
    // return token ? true : false;  // Retorna true se houver token, caso contrário, false
    const token = localStorage.getItem('token');  // Obtém o token do localStorage

  // Verifica se o token existe e tem a estrutura básica de um JWT (3 partes separadas por ponto)
  if (token && token.split('.').length === 3) {
    try {
      // Aqui você pode decodificar e verificar o payload do JWT, se necessário
      const payload = JSON.parse(atob(token.split('.')[1]));  // Decodifica a parte "Payload" do JWT

      // Verifica se a data de expiração do token é maior que a data atual
      const isExpired = new Date(payload.exp * 1000) < new Date(); // A data de expiração está no campo "exp" do JWT
      if (isExpired) {
        console.log("Token expirado");
        return false;  // O token expirou
      }

      return true;  // O token é válido
    } catch (e) {
      console.error('Token inválido', e);
      return false;  // Erro ao decodificar ou processar o token
    }
  }

  return false;  // Não há token ou o token não tem formato válido
  }

  // Método para obter o token
  getToken(): string | null {
    return localStorage.getItem('token');  // Retorna o token armazenado ou null
  }
}
