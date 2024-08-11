import { Component } from '@angular/core';
import {
  LinkComponent,
  TLinkProps,
} from '../../../../components/link/link.component';

@Component({
  selector: 'home-header',
  standalone: true,
  imports: [LinkComponent],
  template: `
    <header class="flex items-center justify-between bg-secondary px-4 pt-4">
      <app-link [props]="appLinkProps" />
      <ul class="flex flex-grow items-center justify-evenly">
        @for (linkProps of headerMenuProps; track linkProps.link) {
          <li>
            <app-link [props]="linkProps" />
          </li>
        }
      </ul>
    </header>
  `,
})
export class HeaderComponent {
  appLinkProps: TLinkProps = {
    link: '/',
    underlineColor: 'primary',
    icon: { name: '', size: 'md', color: 'primary' },
  };

  headerMenuProps: TLinkProps[] = [
    {
      link: 'about',
      underlineColor: 'primary',
      typography: { text: 'About' },
    },
    {
      link: '404',
      underlineColor: 'primary',
      typography: { text: 'Not Found' },
    },
  ];
}
