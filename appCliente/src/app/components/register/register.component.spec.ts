import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule, FormBuilder } from '@angular/forms';
import { Router } from '@angular/router';
import { of, throwError } from 'rxjs';
import { RegisterComponent } from './register.component';
import { ClienteService } from '../../services/cliente.service';
import { AuthService } from '../../services/auth.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { FormsModule } from '@angular/forms';  // Import FormsModule if you're using ngModel in the template

describe('RegisterComponent', () => {
  let component: RegisterComponent;
  let fixture: ComponentFixture<RegisterComponent>;
  let clienteServiceMock: jasmine.SpyObj<ClienteService>;
  let routerMock: jasmine.SpyObj<Router>;

  beforeEach(async () => {
    // Mock do ClienteService e do Router
    clienteServiceMock = jasmine.createSpyObj('ClienteService', ['createCliente']);
    routerMock = jasmine.createSpyObj('Router', ['navigate']);

    await TestBed.configureTestingModule({
      declarations: [ RegisterComponent ],
      imports: [
        ReactiveFormsModule,
        FormsModule,   // Add FormsModule if you're using ngModel in the template
        HttpClientTestingModule
      ],
      providers: [
        { provide: ClienteService, useValue: clienteServiceMock },
        { provide: Router, useValue: routerMock },
        AuthService,
        FormBuilder
      ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RegisterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('deveria criar', () => {
    expect(component).toBeTruthy();
  });
});
