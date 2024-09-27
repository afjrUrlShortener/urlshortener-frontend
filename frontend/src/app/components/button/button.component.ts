import { Component, Input, output } from '@angular/core';
import { IconComponent, TIconProps } from '../icon/icon.component';
import {
  TTypographyProps,
  TypographyComponent,
} from '../typography/typography.component';
import { TColor } from '../../types/common.type';
import { NgClass } from '@angular/common';

export type TButtonProps = {
  typography?: TTypographyProps;
  icon?: TIconProps;
  bgColor?: TColor;
};

@Component({
  selector: 'app-button',
  standalone: true,
  imports: [IconComponent, TypographyComponent, NgClass],
  template: `
    <button
      type="button"
      class="flex items-center justify-evenly rounded px-2 py-2 transition-colors duration-300"
      [ngClass]="{
        'bg-primary': props.bgColor === 'primary',
        'bg-secondary': props.bgColor === 'secondary' || !props.bgColor,
        'bg-tertiary': props.bgColor === 'tertiary',
      }"
      (click)="click.emit()">
      @if (props.typography && props.icon) {
        <app-typography [props]="props.typography" />
        <app-icon class="ml-2" [props]="props.icon" />
      } @else if (props.typography) {
        <app-typography [props]="props.typography" />
      } @else if (props.icon) {
        <app-icon [props]="props.icon" />
      }
    </button>
  `,
})
export class ButtonComponent {
  @Input({ required: true }) props!: TButtonProps;
  click = output();
}
