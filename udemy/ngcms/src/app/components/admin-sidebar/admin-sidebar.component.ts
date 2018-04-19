import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SidebarService } from '../../services/sidebar.service';

@Component({
  selector: 'app-admin-sidebar',
  templateUrl: './admin-sidebar.component.html',
  styleUrls: ['./admin-sidebar.component.css']
})
export class AdminSidebarComponent implements OnInit {

  content: string;
  id: string;
  successMsg = false;

  constructor(
    private router: Router,
    private sidebarSesrvice: SidebarService
  ) { }

  ngOnInit() {
    this.sidebarSesrvice.getSidebar().subscribe(res => {
      this.content = res['contentName'];
      this.id = res['id'];
    });
  }

  editSidebar({value}) {
    this.sidebarSesrvice.putSidebar(value).subscribe(res => {
      this.successMsg = true;
      setTimeout(function() {
        this.successMsg = false;
      }.bind(this), 2000);
    });
  }

}
