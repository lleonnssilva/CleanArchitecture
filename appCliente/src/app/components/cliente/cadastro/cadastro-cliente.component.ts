import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ClienteService } from '../../../services/cliente.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { cepValidator } from '../../validators/cepVlidator';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-cadastro-cliente',
  templateUrl: './cadastro-cliente.component.html',
  styleUrls: ['./cadastro-cliente.component.css']
})
export class CadastroClienteComponent {
  clienteForm: FormGroup;
  erro: string = '';
  erroCadastro: boolean = false; 
  maskCepValue: string = '';
  maskTelefoneValue: string = '';
  inputValue:string='';
  dddValue:string=''
  constructor(private fb: FormBuilder,private clienteService: ClienteService, private router: Router,) {

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


  cadastrarCliente() {
    if (this.clienteForm.valid) {
      // Lógica para cadastro (por exemplo, chamar um serviço)
      console.log(this.clienteForm.value);
       this.erroCadastro = false;
      this.clienteService.createCliente(this.clienteForm.value).subscribe(
      (response) => {
        console.log('Cliente cadastrado com sucesso!', response);
          this.erroCadastro = false;
        // Redireciona para a página de pesquisa de clientes após cadastro
        this.router.navigate(['/pesquisa-cliente']);
      },
      (error: HttpErrorResponse) => {
        this.erroCadastro=true;
        this.erro = error.error.message;
         console.error('Erro no cadastro:', error.message,'errors:',error.error.errors);
      }
    );
    }
  }



  // Método para aplicar a máscara no formato xxxxx-xxx
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
  apenasLetras(event: any): void {
    // Remove tudo o que não for uma letra
    let value = event.target.value.replace(/[^a-zA-Z]/g, ''); // Expressão regular para remover qualquer coisa que não seja letra
    this.inputValue = value; // Atualiza a variável
    event.target.value = value; // Atualiza o valor no campo de input
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
}
