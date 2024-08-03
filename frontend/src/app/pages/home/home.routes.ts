import { Routes } from '@angular/router';
import { AboutComponent } from './pages/about/about.component';
import { ShortenerComponent } from './pages/shortener/shortener.component';

export const homeRoutes: Routes = [
  {
    path: '',
    title: 'Shortener',
    component: ShortenerComponent,
  },
  {
    path: 'about',
    title: 'About',
    component: AboutComponent,
  },
];
