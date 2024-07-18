import { FormControl } from "@angular/forms";

export interface ClienteForm {
  nome: FormControl<string | null>;
  email: FormControl<string | null>;
  cpf: FormControl<string | null>;
  telefone: FormControl<string | null>;
  endereco: FormControl<string | null>;
  observacao?: FormControl<string | null>;
}
