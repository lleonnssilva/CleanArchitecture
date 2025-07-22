import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CadastroClienteComponent } from './cadastro-cliente.component';
import { ReactiveFormsModule, FormBuilder } from '@angular/forms';
import { ClienteService } from '../../../services/cliente.service';
import { Router } from '@angular/router';
import { of, throwError } from 'rxjs';

describe('CadastroComponent', () => {
  let component: CadastroClienteComponent;
  let fixture: ComponentFixture<CadastroClienteComponent>;
  let clienteServiceMock: jasmine.SpyObj<ClienteService>;
  let routerMock: jasmine.SpyObj<Router>;

  beforeEach(async () => {
    // Mock do ClienteService e do Router
    clienteServiceMock = jasmine.createSpyObj('ClienteService', ['createCliente']);
    routerMock = jasmine.createSpyObj('Router', ['navigate']);

    await TestBed.configureTestingModule({
      declarations: [ CadastroClienteComponent ],
      imports: [ ReactiveFormsModule ],
      providers: [
        { provide: ClienteService, useValue: clienteServiceMock },
        { provide: Router, useValue: routerMock },
        FormBuilder
      ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CadastroClienteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('deve criar o componente', () => {
    expect(component).toBeTruthy();
  });

  it('deve criar um formulário com os campos necessários', () => {
    expect(component.clienteForm.contains('id')).toBeTrue();
    expect(component.clienteForm.contains('nome')).toBeTrue();
    expect(component.clienteForm.contains('email')).toBeTrue();
    expect(component.clienteForm.contains('telefone')).toBeTrue();
    expect(component.clienteForm.contains('endereco')).toBeTrue();
  });

  it('deve validar o campo nome como obrigatório', () => {
    const nome = component.clienteForm.get('nome');
    nome?.setValue('');
    expect(nome?.valid).toBeFalse();
  });

  it('deve chamar createCliente quando o formulário for válido e o submit for chamado', () => {
    const clienteMock = {id:'123', nome: 'João', email: { enderecoEmail: 'joao@teste.com' }, telefone: { ddd: '11', numeroTelefone: '123456789' }, endereco: { rua: 'Rua X', numero: '123', bairro: 'Centro', cidade: 'São Paulo', estado: 'SP', cep: '12345-678' } };
    
    // Preenche o formulário com dados válidos
    component.clienteForm.setValue(clienteMock);

    clienteServiceMock.createCliente.and.returnValue(of({ success: true }));

    // Chamando o método onSubmit()
    component.onSubmit();

    expect(clienteServiceMock.createCliente).toHaveBeenCalledWith(clienteMock);
    expect(routerMock.navigate).toHaveBeenCalledWith(['/pesquisa-cliente']);
  });

  it('deve lidar com erro ao submeter o formulário', () => {
    const clienteMock = { id:'123',nome: 'João', email: { enderecoEmail: 'joao@teste.com' }, telefone: { ddd: '11', numeroTelefone: '123456789' }, endereco: { rua: 'Rua X', numero: '123', bairro: 'Centro', cidade: 'São Paulo', estado: 'SP', cep: '12345-678' } };

    // Preenche o formulário com dados válidos
    component.clienteForm.setValue(clienteMock);

    clienteServiceMock.createCliente.and.returnValue(throwError('Erro ao cadastrar cliente'));

    // Chamando o método onSubmit()
    component.onSubmit();

    // A função deve ser chamada, mas o erro não deve impedir o fluxo.
    expect(clienteServiceMock.createCliente).toHaveBeenCalledWith(clienteMock);
  });

  it('deve verificar se o formulário é válido antes de submeter', () => {
    component.clienteForm.setValue({
      id: '1',
      nome: 'Maria',
      email: { enderecoEmail: 'maria@teste.com' },
      telefone: { ddd: '21', numeroTelefone: '987654321' },
      endereco: { rua: 'Rua Y', numero: '456', bairro: 'Zona Sul', cidade: 'Rio de Janeiro', estado: 'RJ', cep: '98765-432' }
    });

    expect(component.clienteForm.valid).toBeTrue();

    const submitSpy = spyOn(component, 'onSubmit');

    // Chamando o método cadastrarCliente que verifica a validade do formulário
    component.cadastrarCliente();

    expect(submitSpy).toHaveBeenCalled();
  });
});
