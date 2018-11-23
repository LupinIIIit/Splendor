import { Component, OnInit, ViewChild } from '@angular/core';
import { MatMenuTrigger } from '@angular/material';
import { User } from '../../models/user.model';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  events: string[] = [];
  opened: boolean;
  @ViewChild(MatMenuTrigger) trigger: MatMenuTrigger;
  user : User;
  someMethod() {
    this.trigger.openMenu();
  }
  constructor(private router: Router,private authService :AuthService) { }
  ngOnInit() {
    if (this.user === null || this.user === undefined) {
      this.user = new User('2', 'tony', 'a');
    }
  }
  Logout() {
    this.authService.logout();
  }
}
