import { Routes } from '@angular/router';
import { logger } from '../../utils/logger.utils';

export const homeRoutes: Routes = [
  {
    path: '',
    title: 'Shortener',
    loadComponent: () =>
      import('./pages/shortener/shortener.component')
        .then(x => x.ShortenerComponent)
        .finally(() => logger.debug('loaded shortener')),
  },
  {
    path: 'about',
    title: 'About',
    loadComponent: () =>
      import('./pages/about/about.component')
        .then(x => x.AboutComponent)
        .finally(() => logger.debug('loaded about')),
  },
];
