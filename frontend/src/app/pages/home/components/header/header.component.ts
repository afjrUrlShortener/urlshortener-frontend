import { Component } from '@angular/core';
import { HeaderMenuComponent } from './components/header-menu/header-menu.component';
import { THeaderMenuItem } from './types/header.types';

@Component({
  selector: 'home-header',
  standalone: true,
  imports: [HeaderMenuComponent],
  template: `
    <header class="bg-secondary p-4">
      <nav>
        <header-menu [headerMenuItems]="headerMenuItems" />
      </nav>
    </header>
  `,
})
export class HeaderComponent {
  headerMenuItems: THeaderMenuItem[] = [
    { text: 'Home', iconName: 'chevron-down' },
    { text: 'About', iconName: 'chevron-down' },
    { text: 'Pricing', iconName: 'chevron-down' },
  ];
}
