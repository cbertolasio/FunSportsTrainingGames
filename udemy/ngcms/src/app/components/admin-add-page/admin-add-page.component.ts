import { Component, OnInit } from '@angular/core';
import { log } from 'util';
import { Router } from '@angular/router';

import { PageService } from '../../services/page.service';

@Component({
  selector: 'app-admin-add-page',
  templateUrl: './admin-add-page.component.html',
  styleUrls: ['./admin-add-page.component.css']
})
export class AdminAddPageComponent implements OnInit {

  successMsg = false;
  errorMsg = false;
  title: string;
  content: string;

  constructor(
    private router: Router,
    private pageService: PageService
  ) { }

  ngOnInit() {
  }

  addPage({form, value, valid}) {
    form.reset();
    if (valid) {
      this.pageService.postAddPage(value).subscribe(res => {
        if (res === 'pageExists') {
          this.errorMsg = true;
          setTimeout(function() {
            this.errorMsg = false;
          }.bind(this), 2000);
        } else {
          this.successMsg = true;
          setTimeout(function() {
            this.successMsg = false;
          }.bind(this), 2000);

          this.pageService.getPages().subscribe(pages => {
            this.pageService.pagesBS.next(pages);
          });
        }
      });
    } else {
      console.log('Form is not valid');
    }
  }
}
