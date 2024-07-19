import { LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MenuComponent } from './components/menu/menu.component';
import { ClientesComponent } from './components/clientes/clientes.component';
import { HttpClientModule } from '@angular/common/http';
import { CadastroClienteComponent } from './components/clientes/cadastro-cliente/cadastro-cliente.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgxMaskDirective, provideNgxMask } from 'ngx-mask';
import { ProdutosComponent } from './components/produtos/produtos.component';
import { CadastroProdutoComponent } from './components/produtos/cadastro-produto/cadastro-produto.component';
import { registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { BaseChartDirective, provideCharts, withDefaultRegisterables } from 'ng2-charts';

registerLocaleData(localePt, 'pt-BR');

@NgModule({
  declarations: [
    AppComponent,
    MenuComponent,
    ClientesComponent,
    CadastroClienteComponent,
    ProdutosComponent,
    CadastroProdutoComponent,
    DashboardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    NgxMaskDirective,
    BaseChartDirective
  ],
  providers: [
    { provide: LOCALE_ID, useValue: 'pt-BR' },
    provideNgxMask(),
    provideCharts(withDefaultRegisterables())
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
