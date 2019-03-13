import { LOCALE_ID, Inject, Component, ViewChild, ElementRef} from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material';
// import { NgElement } from '@angular/elements';/
import { Router } from '@angular/router';
import { MyDialogComponent } from '../my-dialog/my-dialog.component';
import { $ } from 'protractor';


@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  testurl = '/en';
  address = 'en';
  @ViewChild('testurl') testelement: ElementRef;

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

  RequestLanguage() {
    const config = new MatDialogConfig();
    config.width = '300px';
    if (this.localeId === 'sv') {
      config.data =  'Är du säker att byta språket? \r\n Tryck Yes att ändra språket annars tryck No.' ;
    } else if (this.localeId === 'zh-hans') {
      config.data =  '确定要更改语言吗? \r\n 按‘是’，更改语言；否则按‘否’。' ;
    } else {
      this.localeId = 'en';
      config.data =  'Are you sure to change the language?Press Yes to change the language otherwise press No.' ;
    }
    const dialogRef = this.dialog.open(MyDialogComponent, config);
    console.log('dialog is opened');
    dialogRef.afterClosed().subscribe(result => {
      console.log('dialog is closed');
      // console.log('Dialog closed:${result}');
      // this.dialogResult = result;
      if ( result === 'Confirm') {
        this.router.navigate(['/' + this.localeId ]);
      }
    });
    return false;

  }
  testFunction() {
    alert('testFunction is called');
    return false;
  }
  changeUrl() {
    this.testurl = this.address;
    // document.querySelector('testurl').click();
    // this.testelement.nativeElement.href = '/sv';
    alert('changeUrl is running');


  }

  constructor(@Inject(LOCALE_ID) protected localeId: string, private el: ElementRef, public dialog: MatDialog, private router: Router) {
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
