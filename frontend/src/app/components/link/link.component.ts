import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { IconComponent } from '../icon/icon.component';
import { TIconName } from '../../types/common.type';

@Component({
  selector: 'app-link',
  standalone: true,
  imports: [RouterLink, IconComponent],
  template: `
    <a class="group flex flex-col" [routerLink]="link">
      <div class="flex items-center justify-center">
        @if (text && iconName) {
          <span class="font-medium">{{ text }}</span>
          <app-icon class="ml-2 mt-1" [name]="iconName" size="xsm" />
        } @else if (text) {
          <span class="font-medium">{{ text }}</span>
        } @else if (iconName) {
          <app-icon [name]="iconName" />
        }
      </div>
      <div
        class="mt-2 w-0 rounded transition-all duration-300 group-hover:w-full group-hover:outline group-hover:outline-1"></div>
    </a>
  `,
})
export class LinkComponent {
  @Input({ required: true }) link = '';
  @Input() text?: string;
  @Input() iconName?: TIconName;
}
