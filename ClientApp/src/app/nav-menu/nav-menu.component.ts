import { LOCALE_ID, Inject, Component } from '@angular/core';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;

  languageList = [
    { code: 'en', label: 'English' },
    { code: 'sv', label: 'Swedish' },
    { code: 'zh-hans', label: 'Chinese' }
  ];

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }


  constructor(@Inject(LOCALE_ID) protected localeId: string) {
    const now = new Date().getTime(); // 当前时间
    console.log(now + 'localeId');
    console.log(localeId);
    console.log('end');
    if (localeId === 'sv') {
      this.languageList[0].label = 'Engelska';
      this.languageList[1].label = 'Svenska';
      this.languageList[2].label = 'Kinesiska';
    } else if (localeId === 'zh-hans') {
      this.languageList[0].label = '英语';
      this.languageList[1].label = '瑞典语';
      this.languageList[2].label = '中文';
  } else {
    localeId = 'en';
    this.languageList[0].label = 'English';
    this.languageList[1].label = 'Swedish';
    this.languageList[2].label = 'Chinese';

    }
  }
}
