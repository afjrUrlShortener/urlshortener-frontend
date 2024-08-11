import { Component, Input, OnInit } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { NgClass } from '@angular/common';

export type TTextInputProps = {
  label: string;
  placeholder: string;
  formControl: FormControl;
  invalidFormLabel: string;
};

@Component({
  selector: 'app-text-input',
  standalone: true,
  imports: [ReactiveFormsModule, NgClass],
  template: `
    <label class="block font-bold text-secondary" [for]="props.label">
      {{ props.label }}
    </label>
    <div class="h-18">
      <input
        [id]="props.label"
        class="w-full rounded border-2 bg-primary px-4 py-2 outline-none transition-colors duration-300"
        [ngClass]="isFormValid ? 'border-secondary' : 'border-error'"
        type="text"
        [formControl]="props.formControl"
        [placeholder]="props.placeholder" />

      <label
        class="w-full font-medium text-error transition-opacity duration-300"
        [ngClass]="isFormValid ? 'invisible opacity-0' : 'visible'"
        [for]="props.label">
        *{{ props.invalidFormLabel }}
      </label>
    </div>
  `,
})
export class TextInputComponent implements OnInit {
  @Input({ required: true }) props!: TTextInputProps;

  isFormValid = true;
  ngOnInit() {
    this.props.formControl.statusChanges.subscribe(_ => {
      this.isFormValid =
        this.props.formControl.pristine || this.props.formControl.valid;
    });
  }
}
