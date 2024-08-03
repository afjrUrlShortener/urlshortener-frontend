import { Component } from '@angular/core';
import { FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { TextInputComponent } from '../../../../components/text-input/text-input.component';
import { ButtonComponent } from '../../../../components/button/button.component';
import { IconComponent } from '../../../../components/icon/icon.component';

@Component({
  selector: 'app-shortener',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    TextInputComponent,
    ButtonComponent,
    IconComponent,
  ],
  template: `
    <div class="flex flex-col items-start justify-around p-8">
      <app-text-input
        [ngxFormControl]="shortenerFormControl"
        label="Paste your long link here"
        invalidFormLabel="Must be a valid URL"
        placeholder="https://example.com/my-long-url" />
      <app-button
        text="Get your link for free"
        iconName="link"
        (onClick)="onButtonClick()" />
    </div>
  `,
})
export class ShortenerComponent {
  shortenerFormControl = new FormControl('', [
    Validators.required,
    Validators.pattern(/[a-zA-Z]{2,}:\/{2}/),
  ]);

  onButtonClick(): void {
    this.shortenerFormControl.markAsDirty();
    this.shortenerFormControl.markAsTouched();

    if (this.shortenerFormControl.value == '') {
      this.shortenerFormControl.updateValueAndValidity({ onlySelf: true });
    }
  }
}
