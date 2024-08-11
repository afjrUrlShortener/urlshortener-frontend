import { Component } from '@angular/core';
import { NgClass } from '@angular/common';

@Component({
  selector: 'home-footer',
  standalone: true,
  imports: [NgClass],
  template: ` <footer class="h-20 bg-secondary"></footer> `,
})
export class FooterComponent {}
