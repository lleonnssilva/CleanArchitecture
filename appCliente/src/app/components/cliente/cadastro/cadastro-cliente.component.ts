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
         console.error('Erro no cadastro:', error.error.message);
      }
    );
    }
  }
}
