import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { CategoriasComponent } from './categorias/categorias.component';
import { CategoriaDetalheComponent } from './categoria-detalhe/categoria-detalhe.component';
import { CategoriaNovaComponent } from './categoria-nova/categoria-nova.component';
import { CategoriaEditarComponent } from './categoria-editar/categoria-editar.component';
import { LoginComponent } from './login/login.component';
import { LogoutComponent } from './logout/logout.component';

const routes: Routes = [
  {
    path:'login',
    component: LoginComponent,
    data:{title:'Login'}
  },
  {
    path:'logout',
    component: LogoutComponent,
    data:{title:'Logout'}
  },
  {
    path:'categorias',
    component: CategoriasComponent,
    data:{title:'Lista de Categorias'},
  },
  {
    path:'categorias-detalhe/:id',
    component: CategoriaDetalheComponent,
    data:{title:'Detalhe de Categoria'}
  },
  {
    path:'categoria-nova',
    component: CategoriaNovaComponent,
    data:{title:'Adicionar Categoria'}
  },
  {
    path:'categoria-editar/:id',
    component: CategoriaEditarComponent,
    data:{title:'Editar a Categoria'}
  },
  {
    path:'',
    redirectTo:'/categorias',
    pathMatch:'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
