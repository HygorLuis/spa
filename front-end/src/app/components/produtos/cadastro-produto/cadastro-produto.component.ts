import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Produto } from '../../../models/produto.model';
import { ProdutoForm } from './produto-form';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ProdutoService } from '../../../services/produto.service';

@Component({
  selector: 'app-cadastro-produto',
  templateUrl: './cadastro-produto.component.html',
  styleUrl: './cadastro-produto.component.scss'
})

export class CadastroProdutoComponent implements OnInit {
  @Input() produtoSelecionado!: Produto;
  @Output() cardAberto = new EventEmitter<boolean>();

  submitted: boolean = false;
  produto = new Produto();

  produtoForm = new FormGroup<ProdutoForm>({
    nome: new FormControl('', [Validators.required]),
    quantidadeEstoque: new FormControl('', [Validators.required]),
    valorCusto: new FormControl('', [Validators.required]),
    valorVenda: new FormControl('', [Validators.required]),
    observacao: new FormControl('')
  });

  constructor(private service: ProdutoService) {}

  ngOnInit(): void {
    this.produtoForm.valueChanges.subscribe((values) => {
      this.produto.nome = values.nome as string;
      this.produto.quantidadeEstoque = values.quantidadeEstoque as number;
      this.produto.valorCusto = values.valorCusto as number;
      this.produto.valorVenda = values.valorVenda as number;
      this.produto.observacao = values.observacao as string;
    });

    if (this.produtoSelecionado?.id != '') {
      this.produtoForm.patchValue({
        nome: this.produtoSelecionado.nome as string,
        quantidadeEstoque: this.produtoSelecionado.quantidadeEstoque as number,
        valorCusto: this.produtoSelecionado.valorCusto as number,
        valorVenda: this.produtoSelecionado.valorVenda as number,
        observacao: this.produtoSelecionado.observacao as string,
      });

      this.produto = this.produtoSelecionado;
    }
  }

  onSubmit() {
    this.submitted = true;

    if (this.produtoForm.invalid) return;

    if (this.produtoSelecionado.id != '') {
      this.service.atualizar(this.produto).subscribe(() => {
        this.fechar();
      });
    } else {
      this.service.cadastrar(this.produto).subscribe(() => {
        this.fechar();
      });
    }
  }

  fechar(): void {
    this.cardAberto.emit(false);
  }
}
