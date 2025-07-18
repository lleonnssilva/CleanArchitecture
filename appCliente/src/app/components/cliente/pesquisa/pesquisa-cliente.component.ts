import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';  // Para navegação
import { ClienteService } from '../../../services/cliente.service';

@Component({
  selector: 'app-pesquisa-cliente',
  templateUrl: './pesquisa-cliente.component.html',
  styleUrls: ['./pesquisa-cliente.component.css']
})
export class PesquisaClienteComponent implements OnInit {

  pesquisa: string = ''; 
  clientes: any[] = [];   
  clientesFiltrados: any[] = []; 

  constructor(private clienteService: ClienteService, private router: Router) {}

  ngOnInit(): void {
    this.carregarClientes(); 
  }

  carregarClientes() {
    this.clienteService.getClientes().subscribe(
      (data) => {
        this.clientes = data;
        this.clientesFiltrados = data;  
      },
      (error) => {
        console.error('Erro ao carregar clientes:', error);
      }
    );
  }

  buscarClientes() {
    if (this.pesquisa.trim() === '') {
      this.clientesFiltrados = this.clientes;  
    } else {
      this.clientesFiltrados = this.clientes.filter(cliente =>
        cliente.nome.toLowerCase().includes(this.pesquisa.toLowerCase()) ||
        cliente.email.toLowerCase().includes(this.pesquisa.toLowerCase())
      );
    }
  }

  editarCliente(clienteId: number) {
    this.router.navigate([`/atualizar-cliente/${clienteId}`]);  
  }


  excluirCliente(clienteId: number) {
    const confirmar = confirm('Tem certeza que deseja excluir este cliente?');
    if (confirmar) {
      this.clienteService.deleteCliente(clienteId).subscribe(
        () => {
          console.log('Cliente excluído com sucesso!');
          this.carregarClientes();  
        },
        (error) => {
          console.error('Erro ao excluir cliente:', error);
        }
      );
    }
  }
}
