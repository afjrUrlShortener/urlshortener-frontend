import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { TIconName } from '../../types/common.type';
import { ButtonComponent } from '../button/button.component';

@Component({
  selector: 'app-link-button',
  standalone: true,
  imports: [RouterLink, ButtonComponent],
  template: `
    <a [routerLink]="link">
      <app-button [text]="text" [iconName]="iconName" />
    </a>
  `,
})
export class LinkButtonComponent {
  @Input() text?: string;
  @Input() iconName?: TIconName;
  @Input({ required: true }) link = '';
}
