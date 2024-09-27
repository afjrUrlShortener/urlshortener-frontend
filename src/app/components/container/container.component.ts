import { Component, Input } from '@angular/core';
import { TColor } from '../../types/common.type';
import { NgClass } from '@angular/common';

export type TContainerProps = {
  bgColor: TColor;
};

@Component({
  selector: 'app-container',
  standalone: true,
  imports: [NgClass],
  template: `
    <div
      [ngClass]="{
        'bg-primary': props.bgColor === 'primary',
        'bg-secondary': props.bgColor === 'secondary',
        'bg-tertiary': props.bgColor === 'tertiary',
      }">
      <div class="mx-auto max-w-screen-xl">
        <ng-content></ng-content>
      </div>
    </div>
  `,
})
export class ContainerComponent {
  @Input({ required: true }) props!: TContainerProps;
}
