import { Component, Input } from '@angular/core';
import { TColor, TSize } from '../../types/common.type';
import { NgClass } from '@angular/common';

export type TTypographyProps = {
  text: string;
  color?: TColor;
  size?: TSize;
  weight?: 'normal' | 'semi-bold' | 'extra-bold';
};

@Component({
  selector: 'app-typography',
  standalone: true,
  imports: [NgClass],
  template: `
    <span
      [ngClass]="{
        'text-primary': props.color === 'primary',
        'text-secondary': props.color === 'secondary' || !props.color,
        'text-tertiary': props.color === 'tertiary',
        'text-base': props.size === 'md' || !props.size,
        'text-2xl': props.size === 'lg',
        'text-4xl': props.size === 'xlg',
        'font-normal': props.weight === 'normal' || !props.weight,
        'font-semibold': props.weight === 'semi-bold',
        'font-extrabold': props.weight === 'extra-bold',
      }">
      {{ props.text }}
    </span>
  `,
})
export class TypographyComponent {
  @Input({ required: true }) props!: TTypographyProps;
}
