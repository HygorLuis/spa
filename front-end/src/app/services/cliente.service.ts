import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Cliente } from '../models/cliente.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})

export class ClienteService {

  constructor(private httpCliente: HttpClient) { }

  buscar(): Observable<Cliente[]> {
    return this.httpCliente.get<Cliente[]>(`${environment.apiUrl}cliente`);
  }

  cadastrar(cliente: Cliente): Observable<Cliente> {
    return this.httpCliente.post<Cliente>(`${environment.apiUrl}cliente`, cliente)
  }

  atualizar(cliente: Cliente): Observable<Cliente> {
    return this.httpCliente.put<Cliente>(`${environment.apiUrl}cliente/${cliente.id}`, cliente)
  }

  excluir(idCliente: string): Observable<Cliente> {
    return this.httpCliente.delete<Cliente>(`${environment.apiUrl}cliente/${idCliente}`);
  }
}
