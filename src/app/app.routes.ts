import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { logger } from './utils/logger.utils';

export const routes: Routes = [
  {
    path: '',
    title: 'Home',
    component: HomeComponent,
    loadChildren: () =>
      import('./pages/home/home.routes')
        .then(x => x.homeRoutes)
        .finally(() => logger.debug('loaded home')),
  },
  {
    path: '**',
    title: '404',
    loadComponent: () =>
      import('./pages/page-not-found/page-not-found.component')
        .then(x => x.PageNotFoundComponent)
        .finally(() => logger.debug('loaded page not found')),
  },
];
