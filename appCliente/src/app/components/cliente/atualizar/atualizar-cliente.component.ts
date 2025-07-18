import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ClienteService } from '../../../services/cliente.service';

@Component({
  selector: 'app-atualizar-cliente',
  templateUrl: './atualizar-cliente.component.html',
  styleUrls: ['./atualizar-cliente.component.css']
})
export class AtualizarClienteComponent implements OnInit {
  erro: string | null = null; 
  clienteId: number=0;
  cliente: any = { nome: '', email: '', telefone: '' };

  constructor(
    private clienteService: ClienteService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    // Obtém o ID do cliente da URL
    this.route.params.subscribe(params => {
      this.clienteId = params['id'];
      this.loadCliente();
    });
  }

  // Carregar os dados do cliente para editar
  loadCliente() {
    this.clienteService.getClienteById(this.clienteId).subscribe(
      (data) => {
        this.cliente = data;
      },
      (error) => {
        console.error('Erro ao carregar cliente:', error);
      }
    );
  }

  // Método para atualizar os dados do cliente
  atualizar() {
    this.clienteService.updateCliente(this.clienteId, this.cliente).subscribe(
      (response) => {
        console.log('Cliente atualizado com sucesso!', response);
        this.router.navigate(['/pesquisa-cliente']);
      },
      (error) => {
        console.error('Erro ao atualizar cliente:', error);
      }
    );
  }
}
