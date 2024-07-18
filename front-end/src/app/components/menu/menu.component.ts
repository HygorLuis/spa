import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';

const ABA_ATUAL_REGEX = /^\/([^\/]+)/;

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrl: './menu.component.scss'
})

export class MenuComponent implements OnInit{

  activeTab: string | null = '';

    constructor(private router: Router) { }

  ngOnInit() {
    this.atualizarAbaAtiva(this.router.url);
    this.router.events.subscribe(i => {
      if (i instanceof NavigationEnd) {
        this.atualizarAbaAtiva(i.url);
      }
    });
  }

  private atualizarAbaAtiva(url: string) {
    const match = ABA_ATUAL_REGEX.exec(url);
    this.activeTab = match ? match[1] : null;
  }

}
