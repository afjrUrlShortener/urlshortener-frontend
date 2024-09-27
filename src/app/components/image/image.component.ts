import { Component, Input } from '@angular/core';
import { NgClass, NgOptimizedImage } from '@angular/common';
import { TSize } from '../../types/common.type';

export type TImageProps = {
  src: string;
  xSize: TSize;
  ySize: TSize;
  alt: string;
};

@Component({
  selector: 'app-image',
  standalone: true,
  imports: [NgOptimizedImage, NgClass],
  template: `
    <img
      [ngSrc]="props.src"
      width="1"
      height="1"
      [alt]="props.alt"
      [ngClass]="{
        'w-6': props.xSize === 'sm',
        'w-24': props.xSize === 'md',
        'w-48': props.xSize === 'lg',
        'w-96': props.xSize === 'xlg',
        'h-6': props.ySize === 'sm',
        'h-24': props.ySize === 'md',
        'h-48': props.ySize === 'lg',
        'h-96': props.ySize === 'xlg',
      }" />
  `,
})
export class ImageComponent {
  @Input({ required: true }) props!: TImageProps;
}
