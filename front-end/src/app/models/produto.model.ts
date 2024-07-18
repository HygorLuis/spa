export class Produto {
  id: string = '';
  nome: string = '';
  quantidadeEstoque: number = 0;
  valorCusto: number = 0;
  valorVenda: number = 0;
  observacao?: string;
  dataCadastro?: Date;
}
