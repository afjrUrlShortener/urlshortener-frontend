import { Component } from '@angular/core';
import { FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import {
  TextInputComponent,
  TTextInputProps,
} from '../../../../components/text-input/text-input.component';
import {
  ButtonComponent,
  TButtonProps,
} from '../../../../components/button/button.component';
import {
  ContainerComponent,
  TContainerProps,
} from '../../../../components/container/container.component';

@Component({
  selector: 'app-shortener',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    TextInputComponent,
    ButtonComponent,
    ContainerComponent,
  ],
  template: `
    <app-container [props]="containerProps">
      <div class="flex flex-col items-start justify-around">
        <app-text-input [props]="textInputProps" />
        <app-button
          [props]="buttonProps"
          (mouseenter)="onMouseEnterButton($event)"
          (mouseleave)="onMouseLeaveButton($event)"
          (click)="onButtonClick()" />
      </div>
    </app-container>
  `,
})
export class ShortenerComponent {
  containerProps: TContainerProps = {
    bgColor: 'primary',
  };

  textInputProps: TTextInputProps = {
    label: 'Paste your long link here',
    placeholder: 'https://example.com/my-long-url',
    formControl: new FormControl('', [
      Validators.required,
      Validators.pattern(/[a-zA-Z]{2,}:\/{2}/),
    ]),
    invalidFormLabel: 'Must be a valid URL',
  };

  buttonProps: TButtonProps = {
    typography: {
      text: 'Get your link for free',
      weight: 'semi-bold',
      color: 'primary',
    },
    icon: {
      name: 'link',
      size: 'sm',
      color: 'primary',
    },
  };

  onMouseEnterButton(props: TButtonProps) {
    props.bgColor = 'tertiary';

    if (props.typography && props.icon) {
      props.typography.color = 'secondary';
      props.icon.color = 'secondary';
    }
  }

  onMouseLeaveButton(props: TButtonProps) {
    props.bgColor = 'secondary';

    if (props.typography && props.icon) {
      props.typography.color = 'primary';
      props.icon.color = 'primary';
    }
  }

  onButtonClick(): void {
    this.textInputProps.formControl.markAsDirty();
    this.textInputProps.formControl.markAsTouched();

    if (!this.textInputProps.formControl.value) {
      this.textInputProps.formControl.updateValueAndValidity();
    }
  }
}
