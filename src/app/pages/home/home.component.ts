import { Component } from '@angular/core';
import {
  HeaderComponent,
  THeaderProps,
} from './components/header/header.component';
import { FooterComponent } from './components/footer/footer.component';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [HeaderComponent, FooterComponent, RouterOutlet],
  template: `
    <home-header [props]="headerProps" />
    <main>
      <router-outlet />
    </main>
    <home-footer />
  `,
})
export class HomeComponent {
  headerProps: THeaderProps = {
    logo: {
      link: '/',
      hasUnderline: false,
      icon: { name: '', size: 'md', color: 'primary' },
    },
    menuItems: [
      {
        link: 'about',
        hasUnderline: true,
        underlineColor: 'primary',
        typography: { text: 'About', color: 'primary', weight: 'semi-bold' },
      },
      {
        link: '404',
        hasUnderline: true,
        underlineColor: 'primary',
        typography: {
          text: 'Not Found',
          color: 'primary',
          weight: 'semi-bold',
        },
      },
    ],
  };
}
