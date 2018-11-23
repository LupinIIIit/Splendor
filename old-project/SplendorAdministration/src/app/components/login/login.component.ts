import { Component, OnInit, Inject } from '@angular/core';
import { DOCUMENT } from '@angular/platform-browser';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, NavigationExtras } from '@angular/router';
import { AuthService } from '../../services/index.service';
import { LoginView } from '../../models/index.model';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  year: Date = new Date();
  hide = true;
  isLoginError: boolean = false;
  isLinear = false;
  usernameFormGroup: FormGroup;
  passwordFormGroup: FormGroup;
  constructor(@Inject(DOCUMENT) private document: Document
    , private _formBuilder: FormBuilder,
    public authService: AuthService,
    public router: Router
  ) { }
  ngOnInit() {
    this.document.body.classList.add('login-body');
    this.usernameFormGroup = this._formBuilder.group({
      Username: ['', Validators.required]
    });
    this.passwordFormGroup = this._formBuilder.group({
      Password: ['', Validators.required],
      RememberMe: ''
    });
  }
  OnSubmit() {
    let login: LoginView = new LoginView();
    login.UserName = this.usernameFormGroup.value.Username;
    login.Password = this.passwordFormGroup.value.Password;
    login.RemberMe = this.passwordFormGroup.value.RememberMe;
    //this.authService.login(login).subscribe(() => {
    //  console.log("loggato?" +this.authService.isLoggedIn());
    //  if (this.authService.isLoggedIn()) {
    //    let redirect = this.authService.redirectUrl ? this.authService.redirectUrl : '/dashboard';
    //    let navigationExtras: NavigationExtras = {
    //      queryParamsHandling: 'preserve',
    //      preserveFragment: true
    //    };
    //    this.router.navigate([redirect], navigationExtras);
    //  }
    //});
  }
}
