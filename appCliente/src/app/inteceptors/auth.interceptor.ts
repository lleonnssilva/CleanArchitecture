import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // Tente pegar o token do localStorage
    const token = localStorage.getItem('token');
    console.log("intercept token",token);
    // Se o token estiver presente, adicione ao cabeçalho Authorization
    if (token) {
      const cloned = req.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      });
      return next.handle(cloned);
    }
console.log("intercept not tokennn",token);
    // Se não houver token, apenas continue com a requisição original
    return next.handle(req);
  }
}
