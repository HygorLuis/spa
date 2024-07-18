import { Component, OnInit } from '@angular/core';
import { Produto } from '../../models/produto.model';
import { ProdutoService } from '../../services/produto.service';

@Component({
  selector: 'app-produtos',
  templateUrl: './produtos.component.html',
  styleUrl: './produtos.component.scss'
})

export class ProdutosComponent implements OnInit {

  cardCadastroAberto: boolean = false;
  produtos: Produto[] = [];
  produtoSelecionado = new Produto();

  constructor(private service: ProdutoService) {}

  ngOnInit(): void {
    this.buscarProdutos();
  }

  buscarProdutos(): void {
    this.service.buscar().subscribe((produtos) => {
      this.produtos = produtos;
    })
  }

  abrirCardProduto(abrir: boolean, idProduto: string = '') {
    this.produtoSelecionado = idProduto !== '' ? this.produtos.find((p) => p.id === idProduto) as Produto : new Produto();

    this.cardCadastroAberto = abrir;
    this.buscarProdutos();
  }

  excluirProduto(idCliente: string) {
    this.service.excluir(idCliente).subscribe(() => {
      this.buscarProdutos();
    });
  }

}
