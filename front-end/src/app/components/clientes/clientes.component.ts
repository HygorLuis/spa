import { ClienteService } from './../../services/cliente.service';
import { Component, OnInit } from '@angular/core';
import { Cliente } from '../../models/cliente.model';

@Component({
  selector: 'app-clientes',
  templateUrl: './clientes.component.html',
  styleUrl: './clientes.component.scss'
})

export class ClientesComponent implements OnInit{
  cardCadastroAberto: boolean = false;
  clientes: Cliente[] = [];
  clienteSelecionado = new Cliente();

  constructor(private service: ClienteService) {}

  ngOnInit(): void {
    this.buscarClientes();
  }

  formatarCpf(cpf: string): string {
    return (!cpf)? '' : cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');
  }

  formatarTelefone(telefone: string): string {
    if (!telefone) return '';

    if (telefone.length === 10) {
      return telefone.replace(/(\d{2})(\d{4})(\d{4})/, '($1) $2-$3');
    }

    return telefone.replace(/(\d{2})(\d{5})(\d{4})/, '($1) $2-$3');
  }

  buscarClientes(): void {
    this.service.buscar().subscribe((clientes) => {
      this.clientes = clientes;
    })
  }

  abrirCardCliente(abrir: boolean, idCliente: string = '') {
    this.clienteSelecionado = idCliente !== '' ? this.clientes.find((c) => c.id === idCliente) as Cliente : new Cliente();

    this.cardCadastroAberto = abrir;
    this.buscarClientes();
  }

  excluirCliente(idCliente: string) {
    this.service.excluir(idCliente).subscribe(() => {
      this.buscarClientes();
    });
  }

}
