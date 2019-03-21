import { LOCALE_ID, Inject, Component, ViewChild, ElementRef} from '@angular/core';
import { MatDialog, MatDialogConfig } from '@angular/material';
// import { NgElement } from '@angular/elements';/
import { Router, ActivatedRoute } from '@angular/router';
import { MyDialogComponent } from '../my-dialog/my-dialog.component';



@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  EfterDialogClosed = false;
  currentLang = '';

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

  RequestLanguage(lang: string) {
    console.log('RequestLanguage begin===============, lang=' + lang);
    console.log('EfterDialogClosed = ' + this.EfterDialogClosed);
    this.currentLang = lang;
    console.log('200this.currentLang=' + this.currentLang);
    if (this.EfterDialogClosed) {
      this.EfterDialogClosed = false;
      return true;
    } else {
      const config = new MatDialogConfig<Array<string>>();
      config.width = '450px';
      if (this.localeId === 'sv') {
        config.data =  ['Är du säker att byta språket?', 'Tryck Ja att ändra språket annars tryck Nej.'] ;
      } else if (this.localeId === 'zh-hans') {
        config.data =  ['确定要更改语言吗? ', ' 按‘是’，更改语言；否则按‘否’。' ];
      } else {
        this.localeId = 'en';
        config.data =  ['Are you sure to change the language?', 'Press Yes to change the language otherwise press No.'] ;
      }
      const dialogRef = this.dialog.open(MyDialogComponent, config);
      console.log('dialog is opened and this.currentLang=' + this.currentLang);
      dialogRef.afterClosed().subscribe(result => {
        console.log('dialog is closed and this.currentLang=' + this.currentLang);
        // console.log('Dialog closed:${result}');
        // this.dialogResult = result;
        if ( result === 'Confirm') {
          this.EfterDialogClosed = true;
          let anchorId = '';
          switch (lang) {
            // case 'en':
            //    anchorId = 'languageEnglish';
            //    break;
            case 'sv':
              anchorId = 'languageSwedish';
              break;
            case 'zh-hans':
              anchorId = 'languageChinese';
              break;
            default:
              anchorId = 'languageEnglish';

          }
          console.log('anchorId=' + anchorId );
          window.document.getElementById(anchorId).click();    //  this.router.set = 'sv';


        }
      });
    }
    console.log('RequestLanguage end===============, lang=' + lang);
    return false ;


  }
  testFunction() {
    alert('testFunction is called');
    return false;
  }
  changeUrl() {
    // this.testurl = this.address;
    // document.querySelector('testurl').click();
    // this.testelement.nativeElement.href = '/sv';
    // alert('changeUrl is running');
    this.EfterDialogClosed = true;
    window.document.getElementById('languageEnglish').click();
    // this.router.navigateByUrl('/sv');



  }

  constructor(@Inject(LOCALE_ID) public localeId: string, public dialog: MatDialog, private router: Router, private route: ActivatedRoute) {
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


class Infor {
  public Title: string;
  public Content: string;
  constructor (


  ) {

  }
}
