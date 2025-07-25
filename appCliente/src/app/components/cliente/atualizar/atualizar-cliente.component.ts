import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Cliente } from '../../../model/cliente';
import { ClienteService } from '../../../services/cliente.service';
import { cepValidator } from '../../validators/cepVlidator';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-atualizar-cliente',
  templateUrl: './atualizar-cliente.component.html',
  styleUrls: ['./atualizar-cliente.component.css']
})
export class AtualizarClienteComponent implements OnInit {
  clienteForm: FormGroup;
  clienteId: string="";
  cliente: Cliente | null = null;
  erro: string = '';
  sucesso: boolean = false;
  erroDeAtualizacao: boolean = false;
  maskCepValue:string='';
  maskTelefoneValue:string='';
  dddValue:string='';
  inputValue:string=''
  private data: Cliente[] = [];

  constructor(
    private fb: FormBuilder,
    private clienteService: ClienteService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    // Definindo o formGroup e o formControl para o endereço (que é um FormGroup dentro de um FormGroup)
    this.clienteForm = this.fb.group({
      id: ['', Validators.required],
      nome: ['', Validators.required],
      email: this.fb.group({
        enderecoEmail: ['', Validators.required]
      }),
      telefone: this.fb.group({
        ddd: ['', Validators.required],
        numeroTelefone: ['', Validators.required],
      }),
      endereco: this.fb.group({
        rua: ['', Validators.required],
        numero: ['', Validators.required],
        bairro: ['', Validators.required],
        cidade: ['', Validators.required],
        estado: ['', Validators.required],
        cep: ['', Validators.required],
      }),
    });
  }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.clienteId = params['id']; // Pegando o ID do cliente da URL
      this.loadCliente(this.clienteId); // Carregando o cliente para edição
    });
  }

  // Carrega os dados do cliente
  loadCliente(id: string): void {
    this.clienteService.getClienteById(id).subscribe(
      (data) => {
        this.cliente = data;
        //console.log('loadCliente data',data);
        // Preenche o formulário com os dados do cliente
        this.clienteForm.patchValue({
          id: data.id,
          nome: data.nome,
          email: {
            enderecoEmail: data.email.enderecoEmail
          },
          telefone: {
            ddd: data.telefone.ddd,
            numeroTelefone: data.telefone.numeroTelefone,
          },
          endereco: {
            rua: data.endereco.rua,
            numero: data.endereco.numero,
            bairro: data.endereco.bairro,
            cidade: data.endereco.cidade,
            estado: data.endereco.estado,
            cep: data.endereco.cep,
          },
        });
      },
      (error) => {
        this.erro = 'Erro ao carregar cliente.';
        console.error(error);
      }
    );
  }

  // Método para atualizar o cliente
atualizarCliente(): void {
  console.log('valid form ',this.clienteForm)
  if (this.clienteForm.valid) {
    const clienteAtualizado: Cliente = {
      id: this.clienteId,
      nome: this.clienteForm.value.nome,
      telefone: {
        ddd: this.clienteForm.value.telefone.ddd,
        numeroTelefone: this.clienteForm.value.telefone.numeroTelefone,
      },
      email:  this.clienteForm.value.email ,
      endereco: this.clienteForm.value.endereco,
    };

    this.clienteService.updateCliente(this.clienteId, clienteAtualizado).subscribe(
      (data) => {
        this.sucesso = true;
        this.erroDeAtualizacao = false;
        this.router.navigate(['/pesquisa-cliente']);
      },
       (error: HttpErrorResponse) => {
              this.erroDeAtualizacao=true;
              this.erro = error.error.message;
               console.error('Erro na atualização:', error.error.message);
      }
    );
  } else {
    this.erro = 'Por favor, preencha os campos corretamente.';
  }
}

 aplicarMaskCep(event: any): void {
    let value = event.target.value.replace(/\D/g, ''); // Remove tudo o que não for número
    if (value.length > 5) {
      value = value.slice(0, 5) + '-' + value.slice(5, 8); // Formata o valor como xxxxx-xxx
    }
    this.maskCepValue = value; // Atribui o valor formatado à variável
    event.target.value = value; // Atualiza o valor no input
  }
 aplicarMaskTelefone(event: any): void {
    let value = event.target.value.replace(/\D/g, ''); // Remove tudo o que não for número
    if (value.length > 9) {
      value = value.slice(0, 9); // Limita a 9 dígitos
    }
    this.maskTelefoneValue = value; // Atualiza a variável
    event.target.value = value; // Atualiza o campo de input
  }
   validDDDs: string[] = [
    '11', '21', '31', '41', '51', '61', '71', '81', '91', // São Paulo, Rio de Janeiro, etc.
    '62', '63', '64', '65', '66', '67', '68', '69', '71'   // Exemplos de DDDs adicionais
  ];

  // Método para validar DDD
  validateDDD(event: any): void {
    let value = event.target.value.replace(/\D/g, ''); // Remove qualquer coisa que não for número
    if (value.length > 2) {
      value = value.slice(0, 2); // Limita a entrada a dois caracteres
    }
    
    // Verifica se o DDD é válido
    if (this.validDDDs.includes(value)) {
      event.target.style.borderColor = 'green'; // Se for válido, borda verde
    } else if (value.length === 2) {
      event.target.style.borderColor = 'red'; // Se for inválido, borda vermelha
    }

    this.dddValue = value; // Atualiza a variável
    event.target.value = value; // Atualiza o valor no campo
  }
  apenasLetras(event: any): void {
    // Remove tudo o que não for uma letra
    let value = event.target.value.replace(/[^a-zA-Z]/g, ''); // Expressão regular para remover qualquer coisa que não seja letra
    this.inputValue = value; // Atualiza a variável
    event.target.value = value; // Atualiza o valor no campo de input
  }
}
