import { Component, Input } from '@angular/core';
import { HeaderMenuItemComponent } from '../header-menu-item/header-menu-item.component';
import { LinkComponent } from '../../../../../../components/link/link.component';
import { THeaderMenuItem } from '../../types/header.types';

@Component({
  selector: 'header-menu',
  standalone: true,
  imports: [HeaderMenuItemComponent, LinkComponent],
  template: `
    <ul class="flex items-center justify-evenly">
      @for (item of headerMenuItems; track item.text) {
        <li>
          <header-menu-item [text]="item.text" [iconName]="item.iconName" />
        </li>
      }
    </ul>
  `,
})
export class HeaderMenuComponent {
  @Input({ required: true }) headerMenuItems: THeaderMenuItem[] = [];
}
