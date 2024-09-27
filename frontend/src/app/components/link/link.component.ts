import { Component, Input, output } from '@angular/core';
import { RouterLink } from '@angular/router';
import { IconComponent, TIconProps } from '../icon/icon.component';
import { NgClass } from '@angular/common';
import { TColor } from '../../types/common.type';
import {
  TTypographyProps,
  TypographyComponent,
} from '../typography/typography.component';

export type TLinkProps = {
  link: string;
  typography?: TTypographyProps;
  icon?: TIconProps;
  hasUnderline: boolean;
  hovered?: boolean;
  underlineColor?: TColor;
};

@Component({
  selector: 'app-link',
  standalone: true,
  imports: [RouterLink, IconComponent, NgClass, TypographyComponent],
  template: `
    <a
      class="flex flex-col"
      [routerLink]="props.link"
      (mouseenter)="mouseenter.emit(props)"
      (mouseleave)="mouseleave.emit(props)">
      <div class="mb-2 flex items-center justify-center">
        @if (props.typography && props.icon) {
          <app-typography [props]="props.typography" />
          <app-icon class="ml-2 mt-1" [props]="props.icon" />
        } @else if (props.typography) {
          <app-typography [props]="props.typography" />
        } @else if (props.icon) {
          <app-icon [props]="props.icon" />
        }
      </div>

      @if (props.hasUnderline) {
        <div
          class="rounded outline outline-1 duration-300"
          [ngClass]="{
            'outline-primary': props.underlineColor === 'primary',
            'outline-secondary': props.underlineColor === 'secondary',
            'outline-tertiary': props.underlineColor === 'tertiary',
            'w-0 outline-transparent transition-width': !props.hovered,
            'w-full transition-width': props.hovered,
          }"></div>
      }
    </a>
  `,
})
export class LinkComponent {
  @Input({ required: true }) props!: TLinkProps;
  mouseenter = output<TLinkProps>();
  mouseleave = output<TLinkProps>();
}
