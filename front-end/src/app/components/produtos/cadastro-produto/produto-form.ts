import { FormControl } from "@angular/forms";

export interface ProdutoForm {
  nome: FormControl<string | null>;
  quantidadeEstoque: FormControl<string | number | null>;
  valorCusto: FormControl<string | number | null>;
  valorVenda: FormControl<string | number | null>;
  observacao?: FormControl<string | null>;
}
