import { Routes } from '@angular/router';
import { PageNotFoundComponent } from './pages/page-not-found/page-not-found.component';
import { HomeComponent } from './pages/home/home.component';
import { homeRoutes } from './pages/home/home.routes';

export const routes: Routes = [
  {
    path: '',
    title: 'Home',
    component: HomeComponent,
    children: homeRoutes,
  },
  { path: '**', title: '404', component: PageNotFoundComponent },
];
