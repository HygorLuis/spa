import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Produto } from '../models/produto.model';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class ProdutoService {

  constructor(private httpCliente: HttpClient) { }

  buscar(): Observable<Produto[]> {
    return this.httpCliente.get<Produto[]>(`${environment.apiUrl}produto`);
  }

  cadastrar(produto: Produto): Observable<Produto> {
    return this.httpCliente.post<Produto>(`${environment.apiUrl}produto`, produto)
  }

  atualizar(produto: Produto): Observable<Produto> {
    return this.httpCliente.put<Produto>(`${environment.apiUrl}produto/${produto.id}`, produto)
  }

  excluir(idProduto: string): Observable<Produto> {
    return this.httpCliente.delete<Produto>(`${environment.apiUrl}produto/${idProduto}`);
  }
}
