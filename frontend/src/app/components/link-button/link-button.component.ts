import { Component, Input } from '@angular/core';
import { RouterLink } from '@angular/router';
import { ButtonComponent, TButtonProps } from '../button/button.component';

export type TLinkButtonProps = {
  link: string;
  button: TButtonProps;
};

@Component({
  selector: 'app-link-button',
  standalone: true,
  imports: [RouterLink, ButtonComponent],
  template: `
    <a [routerLink]="props.link">
      <app-button [props]="props.button" />
    </a>
  `,
})
export class LinkButtonComponent {
  @Input({ required: true }) props!: TLinkButtonProps;
}
