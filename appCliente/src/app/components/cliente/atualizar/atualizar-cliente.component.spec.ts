import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AtualizarClienteComponent } from './atualizar-cliente.component';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { ClienteService } from '../../../services/cliente.service';
import { ActivatedRoute, Router } from '@angular/router';
import { of, throwError } from 'rxjs';
import { Cliente } from '../../../model/cliente';

describe('AtualizarClienteComponent', () => {
  let component: AtualizarClienteComponent;
  let fixture: ComponentFixture<AtualizarClienteComponent>;
  let clienteService: jasmine.SpyObj<ClienteService>;
  let router: jasmine.SpyObj<Router>;
  let activatedRoute: jasmine.SpyObj<ActivatedRoute>;

  beforeEach(() => {
    const clienteServiceSpy = jasmine.createSpyObj('ClienteService', ['getClienteById', 'updateCliente']);
    const routerSpy = jasmine.createSpyObj('Router', ['navigate']);
    const activatedRouteSpy = jasmine.createSpyObj('ActivatedRoute', ['params']);
    
    TestBed.configureTestingModule({
      imports: [ReactiveFormsModule, FormsModule],
      declarations: [AtualizarClienteComponent],
      providers: [
        { provide: ClienteService, useValue: clienteServiceSpy },
        { provide: Router, useValue: routerSpy },
        { provide: ActivatedRoute, useValue: activatedRouteSpy },
      ]
    });

    fixture = TestBed.createComponent(AtualizarClienteComponent);
    component = fixture.componentInstance;
    clienteService = TestBed.inject(ClienteService) as jasmine.SpyObj<ClienteService>;
    router = TestBed.inject(Router) as jasmine.SpyObj<Router>;
    activatedRoute = TestBed.inject(ActivatedRoute) as jasmine.SpyObj<ActivatedRoute>;
  });

  it('deve criar o componente', () => {
    expect(component).toBeTruthy();
  });

  it('deve carregar os dados do cliente no init', () => {
    const clienteMock: Cliente = {
      id: '1',
      nome: 'João Silva',
      telefone: { ddd: '11', numeroTelefone: '987654321' },
      email: { enderecoEmail: 'joao@example.com' },
      endereco: {
        rua: 'Rua A',
        numero: '123',
        bairro: 'Bairro A',
        cidade: 'Cidade A',
        estado: 'SP',
        cep: '12345678'
      }
    };
    
    activatedRoute.params = of({ id: '1' });
    clienteService.getClienteById.and.returnValue(of(clienteMock));

    fixture.detectChanges();

    expect(clienteService.getClienteById).toHaveBeenCalledWith('1');
    expect(component.cliente).toEqual(clienteMock);
    expect(component.clienteForm.value.nome).toBe('João Silva');
    expect(component.clienteForm.value.email.enderecoEmail).toBe('joao@example.com');
  });

  it('deve mostrar mensagem de erro quando loadCliente falhar', () => {
    activatedRoute.params = of({ id: '1' });
    clienteService.getClienteById.and.returnValue(throwError('Error'));

    fixture.detectChanges();

    expect(component.erro).toBe('Erro ao carregar cliente.');
  });

  it('deve atualizar o cliente e navegar no sucesso', () => {
    const clienteMock: Cliente = {
      id: '1',
      nome: 'João Silva',
      telefone: { ddd: '11', numeroTelefone: '987654321' },
      email: { enderecoEmail: 'joao@example.com' },
      endereco: {
        rua: 'Rua A',
        numero: '123',
        bairro: 'Bairro A',
        cidade: 'Cidade A',
        estado: 'SP',
        cep: '12345678'
      }
    };
    
    activatedRoute.params = of({ id: '1' });
    clienteService.getClienteById.and.returnValue(of(clienteMock));
    clienteService.updateCliente.and.returnValue(of(clienteMock));

    fixture.detectChanges();

    component.atualizarCliente();

    expect(clienteService.updateCliente).toHaveBeenCalledWith('1', clienteMock);
    expect(router.navigate).toHaveBeenCalledWith(['/pesquisa-cliente']);
    expect(component.sucesso).toBeTrue();
  });

  it('deve mostrar mensagem de erro quando o atualizarCliente falhar', () => {
    const clienteMock: Cliente = {
      id: '1',
      nome: 'João Silva',
      telefone: { ddd: '11', numeroTelefone: '987654321' },
      email: { enderecoEmail: 'joao@example.com' },
      endereco: {
        rua: 'Rua A',
        numero: '123',
        bairro: 'Bairro A',
        cidade: 'Cidade A',
        estado: 'SP',
        cep: '12345678'
      }
    };

    activatedRoute.params = of({ id: '1' });
    clienteService.getClienteById.and.returnValue(of(clienteMock));
    clienteService.updateCliente.and.returnValue(throwError('Error'));

    fixture.detectChanges();

    component.atualizarCliente();

    expect(component.erroDeAtualizacao).toBeTrue();
  });

  it('deve mostrar mensagem de erro de validação de formulário quando o formulário for inválido', () => {
    component.clienteForm.controls['nome'].setValue('');
    component.atualizarCliente();

    expect(component.erro).toBe('Por favor, preencha os campos corretamente.');
  });
});
