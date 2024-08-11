import { Component } from '@angular/core';
import {
  IconComponent,
  TIconProps,
} from '../../components/icon/icon.component';
import {
  LinkButtonComponent,
  TLinkButtonProps,
} from '../../components/link-button/link-button.component';
import {
  TTypographyProps,
  TypographyComponent,
} from '../../components/typography/typography.component';
import { ButtonComponent } from '../../components/button/button.component';

@Component({
  selector: 'app-page-not-found',
  standalone: true,
  imports: [
    IconComponent,
    LinkButtonComponent,
    TypographyComponent,
    ButtonComponent,
  ],
  template: `
    <div class="flex h-screen flex-col items-center justify-center p-8">
      <app-icon
        class="rounded-full border-8 border-secondary p-8"
        [props]="iconProps" />
      <app-typography class="my-10" [props]="typographyProps" />
      <app-link-button
        [props]="linkButtonProps"
        (mouseenter)="onMouseEnterButton()"
        (mouseleave)="onMouseLeaveButton()" />
    </div>
  `,
})
export class PageNotFoundComponent {
  iconProps: TIconProps = {
    name: 'hand-raised',
    size: 'xlg',
    color: 'secondary',
  };

  typographyProps: TTypographyProps = {
    text: "You're not supposed to be here!",
    size: 'lg',
    weight: 'semi-bold',
  };

  linkButtonProps: TLinkButtonProps = {
    link: '/',
    button: {
      typography: {
        text: 'Go back to safety',
        color: 'primary',
        weight: 'semi-bold',
      },
    },
  };

  onMouseEnterButton() {
    this.linkButtonProps.button.bgColor = 'tertiary';
    this.linkButtonProps.button.typography!.color = 'secondary';
  }

  onMouseLeaveButton() {
    this.linkButtonProps.button.bgColor = 'secondary';
    this.linkButtonProps.button.typography!.color = 'primary';
  }
}
