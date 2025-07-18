import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';


@Injectable({
  providedIn: 'root'
})
export class ClienteService {

  private apiUrl = `${environment.apiUrl}/clientes`; // URL base da API de clientes

  constructor(private http: HttpClient) { }

  // Método para obter todos os clientes
  getClientes(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  // Método para criar um novo cliente
  createCliente(cliente: any): Observable<any> {
    return this.http.post<any>(this.apiUrl, cliente);
  }

  // Método para atualizar um cliente
  updateCliente(clienteId: number, cliente: any): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${clienteId}`, cliente);
  }

  // Método para excluir um cliente
  deleteCliente(clienteId: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${clienteId}`);
  }

  // Método para obter um cliente específico pelo ID
  getClienteById(clienteId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${clienteId}`);
  }
}
