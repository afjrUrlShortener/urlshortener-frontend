import { Component, Input } from '@angular/core';
import { NgClass } from '@angular/common';
import { TColor, TIconName, TSize } from '../../types/common.type';

export type TIconProps = {
  name: TIconName;
  size: TSize;
  color: TColor;
};

@Component({
  selector: 'app-icon',
  standalone: true,
  imports: [NgClass],
  template: `
    <svg
      [ngClass]="{
        'size-3': props.size === 'xsm',
        'size-6': props.size === 'sm',
        'size-12': props.size === 'md',
        'size-24': props.size === 'lg',
        'size-48': props.size === 'xlg',
        'stroke-primary': props.color === 'primary',
        'stroke-secondary': props.color === 'secondary',
        'stroke-tertiary': props.color === 'tertiary',
      }"
      xmlns="http://www.w3.org/2000/svg"
      fill="none"
      viewBox="0 0 24 24"
      stroke-width="1.5">
      @switch (props.name) {
        @case ('home') {
          <path
            stroke-linecap="round"
            stroke-linejoin="round"
            d="m2.25 12 8.954-8.955c.44-.439 1.152-.439 1.591 0L21.75 12M4.5 9.75v10.125c0 .621.504 1.125 1.125 1.125H9.75v-4.875c0-.621.504-1.125 1.125-1.125h2.25c.621 0 1.125.504 1.125 1.125V21h4.125c.621 0 1.125-.504 1.125-1.125V9.75M8.25 21h8.25" />
        }
        @case ('bars-4') {
          <path
            stroke-linecap="round"
            stroke-linejoin="round"
            d="M3.75 5.25h16.5m-16.5 4.5h16.5m-16.5 4.5h16.5m-16.5 4.5h16.5" />
        }
        @case ('hand-raised') {
          <path
            stroke-linecap="round"
            stroke-linejoin="round"
            d="M10.05 4.575a1.575 1.575 0 1 0-3.15 0v3m3.15-3v-1.5a1.575 1.575 0 0 1 3.15 0v1.5m-3.15 0 .075 5.925m3.075.75V4.575m0 0a1.575 1.575 0 0 1 3.15 0V15M6.9 7.575a1.575 1.575 0 1 0-3.15 0v8.175a6.75 6.75 0 0 0 6.75 6.75h2.018a5.25 5.25 0 0 0 3.712-1.538l1.732-1.732a5.25 5.25 0 0 0 1.538-3.712l.003-2.024a.668.668 0 0 1 .198-.471 1.575 1.575 0 1 0-2.228-2.228 3.818 3.818 0 0 0-1.12 2.687M6.9 7.575V12m6.27 4.318A4.49 4.49 0 0 1 16.35 15m.002 0h-.002" />
        }
        @case ('chevron-down') {
          <path
            stroke-linecap="round"
            stroke-linejoin="round"
            d="m19.5 8.25-7.5 7.5-7.5-7.5" />
        }
        @case ('link') {
          <path
            stroke-linecap="round"
            stroke-linejoin="round"
            d="M13.19 8.688a4.5 4.5 0 0 1 1.242 7.244l-4.5 4.5a4.5 4.5 0 0 1-6.364-6.364l1.757-1.757m13.35-.622 1.757-1.757a4.5 4.5 0 0 0-6.364-6.364l-4.5 4.5a4.5 4.5 0 0 0 1.242 7.244" />
        }
        @default {
          <path
            d="M13.3333 18.6667L20 12M20 12L13.3333 5.33333M20 12L12 20L4 12M20 12L12 4L4 12M4 18.6667L10.6667 12L4 5.33333M10.6667 18.6667L4 12M4 12L10.6667 5.33333M20 18.6667L13.3333 12L20 5.33333"
            stroke-linecap="round"
            stroke-linejoin="round" />
        }
      }
    </svg>
  `,
})
export class IconComponent {
  @Input({ required: true }) props!: TIconProps;
}
