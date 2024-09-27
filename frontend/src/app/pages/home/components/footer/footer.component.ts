import { Component } from '@angular/core';
import { NgClass } from '@angular/common';
import {
  ContainerComponent,
  TContainerProps,
} from '../../../../components/container/container.component';

@Component({
  selector: 'home-footer',
  standalone: true,
  imports: [NgClass, ContainerComponent],
  template: `
    <app-container [props]="containerProps">
      <footer class="h-20"></footer>
    </app-container>
  `,
})
export class FooterComponent {
  containerProps: TContainerProps = {
    bgColor: 'secondary',
  };
}
