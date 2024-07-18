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
