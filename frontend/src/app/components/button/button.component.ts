import { Component, Input, output } from '@angular/core';
import { IconComponent } from '../icon/icon.component';
import { TIconName } from '../../types/common.type';

@Component({
  selector: 'app-button',
  standalone: true,
  imports: [IconComponent],
  template: `
    <button
      type="button"
      class="group flex items-center justify-evenly rounded bg-tertiary px-2 py-2 transition-all duration-300 hover:bg-secondary"
      (click)="onClick.emit()">
      @if (text && iconName) {
        <span class="font-bold text-secondary group-hover:text-primary">
          {{ text }}
        </span>
        <app-icon
          class="ml-2 stroke-secondary group-hover:stroke-primary"
          [name]="iconName" />

      } @else if (text) {
        <span class="font-bold text-secondary group-hover:text-primary">
          {{ text }}
        </span>

      } @else if (iconName) {
        <app-icon
          class="stroke-secondary group-hover:stroke-primary"
          [name]="iconName" />
      }
    </button>
  `,
})
export class ButtonComponent {
  @Input() text?: string;
  @Input() iconName?: TIconName;
  onClick = output();
}
