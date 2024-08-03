import { Component } from '@angular/core';
import { IconComponent } from '../../components/icon/icon.component';
import { LinkButtonComponent } from '../../components/link-button/link-button.component';

@Component({
  selector: 'app-page-not-found',
  standalone: true,
  imports: [IconComponent, LinkButtonComponent],
  template: `
    <div class="flex h-screen flex-col items-center justify-center p-8">
      <app-icon
        class="rounded-full border-8 border-error stroke-error p-8"
        name="hand-raised"
        size="xlg" />
      <span class="my-10 text-2xl font-medium">
        You're not supposed to be here!
      </span>
      <app-link-button link="/" text="Go back to safety" />
    </div>
  `,
})
export class PageNotFoundComponent {}
