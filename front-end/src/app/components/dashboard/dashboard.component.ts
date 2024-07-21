import { Component, OnInit } from '@angular/core';
import { ChartData, ChartOptions, plugins } from 'chart.js';
import { ProdutoService } from '../../services/produto.service';
import { Produto } from '../../models/produto.model';
import { ClienteService } from '../../services/cliente.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss',
})

export class DashboardComponent implements OnInit {

  readonly colorsTransparent = [
    'rgb(255, 99, 132, 0.2)',  // Red
    'rgb(75, 192, 192, 0.2)',  // Teal
    'rgb(255, 205, 86, 0.2)',  // Yellow
    'rgb(201, 203, 207, 0.2)', // Grey
    'rgb(54, 162, 235, 0.2)',  // Blue
    'rgb(153, 102, 255, 0.2)', // Purple
    'rgb(255, 159, 64, 0.2)',  // Orange
    'rgb(255, 99, 71, 0.2)',   // Tomato
    'rgb(0, 204, 255, 0.2)',   // Sky Blue
    'rgb(102, 255, 178, 0.2)'  // Mint Green
  ];

  readonly colorsSolid = [
    'rgb(255, 99, 132, 1)',  // Red
    'rgb(75, 192, 192, 1)',  // Teal
    'rgb(255, 205, 86, 1)',  // Yellow
    'rgb(201, 203, 207, 1)', // Grey
    'rgb(54, 162, 235, 1)',  // Blue
    'rgb(153, 102, 255, 1)', // Purple
    'rgb(255, 159, 64, 1)',  // Orange
    'rgb(255, 99, 71, 1)',   // Tomato
    'rgb(0, 204, 255, 1)',   // Sky Blue
    'rgb(102, 255, 178, 1)'  // Mint Green
  ];

  polarAreaChartProdutosComMaiorEstoque: ChartData<'polarArea'> = {
    labels: [],
    datasets: [
      {
        data: []
      }
    ]
  };

  barChartProdutosSemEstoque: ChartData<'bar'> = {
    labels: [],
    datasets: [
      {
        data: []
      }
    ]
  };

  barChartOptions: ChartOptions<'bar'> = {
    indexAxis: 'y',
    plugins: {
      legend: {
        display: false
      }
    }
  };

  qtdProdutos: number = 0;
  qtdClientes: number = 0;

  constructor(private produtoService: ProdutoService, private clienteService: ClienteService) { }

  ngOnInit(): void {
    this.produtoService.buscar().subscribe((produtos) => {
      this.produtosComMaiorEstoque(produtos);
      this.buscarProdutosSemEstoque(produtos);
      this.qtdProdutos = produtos.length;
    });

    this.clienteService.buscar().subscribe((clientes) => {
      this.qtdClientes = clientes.length;
    });
  }

  produtosComMaiorEstoque(produtos: Produto[]): void {
     let produtosOrdenados = produtos.sort((a, b) => b.quantidadeEstoque - a.quantidadeEstoque);
     let top10Produtos = produtosOrdenados.slice(0, 10);

     this.polarAreaChartProdutosComMaiorEstoque = {
       labels: top10Produtos.map(x => x.nome),
       datasets: [
         {
           data: top10Produtos.map(x => x.quantidadeEstoque),
           backgroundColor: this.colorsTransparent,
           borderColor: this.colorsSolid,
           borderWidth: 2,
         }
       ]
     };
  }

  buscarProdutosSemEstoque(produtos: Produto[]): void {
    let produtosSemEstoque = produtos.filter(p => p.quantidadeEstoque <= 0);

    this.barChartProdutosSemEstoque = {
      labels: produtosSemEstoque.map(x => x.nome),
      datasets: [
        {
          label: '',
          data: produtosSemEstoque.map(x => x.quantidadeEstoque),
          backgroundColor: this.colorsSolid,
          minBarLength: 2
        }
      ]
    };
  }
}
