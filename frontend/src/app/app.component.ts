import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  template: `
    <main class="text-3xl font-bold underline">
      <h1>{{ title }}</h1>
      <router-outlet />
    </main>`,
  styles: ``,
})
export class AppComponent {
  title = 'url-shortener';
}
