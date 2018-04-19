import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {


  userExists = false;

  constructor(
    private router: Router,
    private userService: UserService
  ) { }

  ngOnInit() {
  }

}
