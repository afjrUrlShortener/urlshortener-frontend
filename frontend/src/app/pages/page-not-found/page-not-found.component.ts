import { Component } from '@angular/core';
import {
  IconComponent,
  TIconProps,
} from '../../components/icon/icon.component';
import {
  TTypographyProps,
  TypographyComponent,
} from '../../components/typography/typography.component';
import {
  ButtonComponent,
  TButtonProps,
} from '../../components/button/button.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-page-not-found',
  standalone: true,
  imports: [IconComponent, TypographyComponent, ButtonComponent],
  template: `
    <div class="flex h-screen flex-col items-center justify-center p-8">
      <app-icon
        class="rounded-full border-8 border-secondary p-8"
        [props]="iconProps" />
      <app-typography class="my-10" [props]="typographyProps" />
      <app-button
        [props]="buttonProps"
        (mouseenter)="onMouseEnterButton($event)"
        (mouseleave)="onMouseLeaveButton($event)"
        (click)="onButtonClick()" />
    </div>
  `,
})
export class PageNotFoundComponent {
  private readonly _router: Router;

  constructor(private readonly router: Router) {
    this._router = router;
  }

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

  buttonProps: TButtonProps = {
    typography: {
      text: 'Go back to safety',
      color: 'primary',
      weight: 'semi-bold',
    },
  };

  onMouseEnterButton(props: TButtonProps) {
    props.bgColor = 'tertiary';

    if (props.typography) {
      props.typography.color = 'secondary';
    }
  }

  onMouseLeaveButton(props: TButtonProps) {
    props.bgColor = 'secondary';

    if (props.typography) {
      props.typography.color = 'primary';
    }
  }

  async onButtonClick() {
    await this._router.navigateByUrl('/');
  }
}
