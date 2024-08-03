import { Component, Input } from '@angular/core';
import { IconComponent } from '../../../../../../components/icon/icon.component';
import { TIconName } from '../../../../../../types/common.type';

@Component({
  selector: 'header-menu-item',
  standalone: true,
  imports: [IconComponent],
  template: `
    <div class="group flex cursor-pointer flex-col">
      <div class="flex items-center justify-center">
        <span class="font-medium text-primary">{{ text }}</span>
        <app-icon
          class="ml-2 mt-1 stroke-primary transition-transform duration-300 group-hover:rotate-180"
          [name]="iconName"
          size="xsm" />
      </div>
      <div
        class="mt-2 w-0 rounded outline-primary transition-all duration-300 group-hover:w-full group-hover:outline group-hover:outline-1"></div>
    </div>
  `,
})
export class HeaderMenuItemComponent {
  @Input({ required: true }) text = '';
  @Input({ required: true }) iconName: TIconName = '';
}
