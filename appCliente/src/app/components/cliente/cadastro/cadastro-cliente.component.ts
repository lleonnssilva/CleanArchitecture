import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ClienteService } from '../../../services/cliente.service';

@Component({
  selector: 'app-cadastro-cliente',
  templateUrl: './cadastro-cliente.component.html',
  styleUrls: ['./cadastro-cliente.component.css']
})
export class CadastroComponent {

  cliente = { nome: '', email: '', telefone: '' };  // Dados do cliente a serem cadastrados

  constructor(private clienteService: ClienteService, private router: Router) {}

  // Método para enviar os dados para o serviço de cadastro
  onSubmit() {
    this.clienteService.createCliente(this.cliente).subscribe(
      (response) => {
        console.log('Cliente cadastrado com sucesso!', response);
        // Redireciona para a página de pesquisa de clientes após cadastro
        this.router.navigate(['/pesquisa-cliente']);
      },
      (error) => {
        console.error('Erro ao cadastrar cliente:', error);
      }
    );
  }
}
