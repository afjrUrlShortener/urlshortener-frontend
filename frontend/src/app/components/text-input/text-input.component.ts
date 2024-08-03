import { Component, Input, OnInit } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { NgClass } from '@angular/common';

@Component({
  selector: 'app-text-input',
  standalone: true,
  imports: [ReactiveFormsModule, NgClass],
  template: `
    <label class="block font-bold text-secondary" [for]="label">
      {{ label }}
    </label>
    <div class="h-18">
      <input
        [id]="label"
        class="w-full rounded border-2 bg-primary px-4 py-2 outline-none transition-colors duration-300"
        [ngClass]="isFormValid ? 'border-secondary' : 'border-error'"
        type="text"
        [formControl]="ngxFormControl"
        [placeholder]="placeholder" />

      <label
        class="w-full font-medium text-error transition-opacity duration-300"
        [ngClass]="isFormValid ? 'invisible opacity-0' : 'visible'"
        [for]="label">
        *{{ invalidFormLabel }}
      </label>
    </div>
  `,
})
export class TextInputComponent implements OnInit {
  @Input({ required: true }) label = '';
  @Input({ required: true }) placeholder = '';
  @Input({ required: true }) ngxFormControl = new FormControl('');
  @Input({ required: true }) invalidFormLabel = '';

  isFormValid = true;
  ngOnInit() {
    this.ngxFormControl.statusChanges.subscribe(_ => {
      this.isFormValid =
        this.ngxFormControl.pristine || this.ngxFormControl.valid;
    });
  }
}
