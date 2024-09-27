import { Component, Input } from '@angular/core';
import {
  LinkComponent,
  TLinkProps,
} from '../../../../components/link/link.component';
import {
  ContainerComponent,
  TContainerProps,
} from '../../../../components/container/container.component';

export type THeaderProps = {
  logo: TLinkProps;
  menuItems: TLinkProps[];
};

@Component({
  selector: 'home-header',
  standalone: true,
  imports: [LinkComponent, ContainerComponent],
  template: `
    <app-container [props]="containerProps">
      <header class="flex items-center justify-between pb-1 pt-4">
        <app-link [props]="props.logo" />
        <ul class="flex flex-grow items-center justify-evenly">
          @for (menuItem of props.menuItems; track menuItem.link) {
            <li>
              <app-link
                [props]="menuItem"
                (mouseenter)="onMouseEnterLink($event)"
                (mouseleave)="onMouseLeaveLink($event)" />
            </li>
          }
        </ul>
      </header>
    </app-container>
  `,
})
export class HeaderComponent {
  @Input({ required: true }) props!: THeaderProps;

  containerProps: TContainerProps = {
    bgColor: 'secondary',
  };

  onMouseEnterLink(props: TLinkProps) {
    props.hovered = true;
  }

  onMouseLeaveLink(props: TLinkProps) {
    props.hovered = false;
  }
}
