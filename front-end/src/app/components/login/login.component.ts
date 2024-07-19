import { Component } from '@angular/core';
import { Login } from '../../models/login.model';
import { LoginService } from '../../services/login.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})

export class LoginComponent {

  login = new Login();
  erro: string = '';

  constructor(private router: Router, private service: LoginService) {}

  onLogin() {
    this.service.authenticate(this.login).subscribe({
      next: (value) => {
        if (value) {
          this.router.navigate(['dashboard']);
        }
      },
      error: (error) => {
        this.erro = 'Usuário ou senha incorretos'
      },
      complete: () => {}
    });
  }

}
