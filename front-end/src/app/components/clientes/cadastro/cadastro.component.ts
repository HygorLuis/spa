import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Cliente } from '../../../models/cliente.model';
import { ClienteService } from '../../../services/cliente.service';
import { ClienteForm } from './cliente-form';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-cadastro',
  templateUrl: './cadastro.component.html',
  styleUrl: './cadastro.component.scss'
})
export class CadastroComponent implements OnInit {
  @Input() clienteSelecionado!: Cliente;
  @Output() cardAberto = new EventEmitter<boolean>();

  submitted: boolean = false;
  cliente = new Cliente();

  clienteForm = new FormGroup<ClienteForm>({
    nome: new FormControl('', [Validators.required]),
    cpf: new FormControl('', [Validators.required, Validators.pattern(/^\d{11}$/)]),
    telefone: new FormControl('', [Validators.required, Validators.pattern(/^\d{10,11}$/)]),
    email: new FormControl('', [Validators.required, Validators.email]),
    endereco: new FormControl('', [Validators.required]),
    observacao: new FormControl('')
  });

  constructor(private service: ClienteService) {}

  ngOnInit(): void {
    this.clienteForm.valueChanges.subscribe((values) => {
      this.cliente.nome = values.nome as string;
      this.cliente.cpf = values.cpf as string;
      this.cliente.telefone = values.telefone as string;
      this.cliente.email = values.email as string;
      this.cliente.endereco = values.endereco as string;
      this.cliente.observacao = values.observacao as string;
    });

    if (this.clienteSelecionado?.id != '') {
      this.clienteForm.patchValue({
        nome: this.clienteSelecionado.nome as string,
        cpf: this.clienteSelecionado.cpf as string,
        telefone: this.clienteSelecionado.telefone as string,
        email: this.clienteSelecionado.email as string,
        endereco: this.clienteSelecionado.endereco as string,
        observacao: this.clienteSelecionado.observacao as string,
      });

      this.cliente = this.clienteSelecionado;
    }
  }

  onSubmit() {
    this.submitted = true;

    if (this.clienteForm.invalid) return;

    if (this.clienteSelecionado.id != '') {
      this.service.atualizar(this.cliente).subscribe(() => {
        this.fechar();
      });
    } else {
      this.service.cadastrar(this.cliente).subscribe(() => {
        this.fechar();
      });
    }
  }

  fechar(): void {
    this.cardAberto.emit(false);
  }
}
