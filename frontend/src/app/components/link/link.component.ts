import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { IconComponent } from '../icon/icon.component';
import { TIconName } from '../../types/common.type';

@Component({
  selector: 'app-link',
  standalone: true,
  imports: [RouterLink, IconComponent],
  template: `
    <a class="flex items-center justify-evenly" [routerLink]="link">
      @if (text && iconName) {
        <span class="font-medium">{{ text }}</span>
        <app-icon [name]="iconName" />
      } @else if (text) {
        <span class="font-medium">{{ text }}</span>
      } @else if (iconName) {
        <app-icon [name]="iconName" />
      }
    </a>
  `,
})
export class LinkComponent {
  @Input({ required: true }) link = '';
  @Input() text?: string;
  @Input() iconName?: TIconName;
}
