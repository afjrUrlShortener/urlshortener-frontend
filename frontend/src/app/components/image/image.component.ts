import { Component, Input } from '@angular/core';
import { NgClass, NgOptimizedImage } from '@angular/common';
import { TSize } from '../../types/common.type';

@Component({
  selector: 'app-image',
  standalone: true,
  imports: [NgOptimizedImage, NgClass],
  template: `
    <img
      [ngSrc]="src"
      width="1"
      height="1"
      [alt]="alt"
      [ngClass]="{
        'w-6': xSize === 'sm',
        'w-24': xSize === 'md',
        'w-48': xSize === 'lg',
        'w-96': xSize === 'xlg',
        'h-6': ySize === 'sm',
        'h-24': ySize === 'md',
        'h-48': ySize === 'lg',
        'h-96': ySize === 'xlg',
      }" />
  `,
})
export class ImageComponent {
  @Input({ required: true }) src = '';
  @Input({ required: true }) xSize: TSize = 'sm';
  @Input({ required: true }) ySize: TSize = 'sm';
  @Input() alt = '';
}
