import { Component } from '@angular/core';
import { LoginService } from '../../services/login.service';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.scss'
})

export class MenuComponent {
    constructor(private loginService: LoginService) { }

    logout(): void {
      this.loginService.logout();
    }
}
