import { AfterViewInit, Component, NgZone, OnInit, Renderer2 } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CredentialResponse } from 'google-one-tap';
import { DividerModule } from 'primeng/divider';
import { environment } from 'src/environments/environment';
import { AuthService, User } from '../services/auth.service';
import { Router } from '@angular/router';
import { DateOfBirthValidator, PasswordLengthValidator, PasswordMatch, PasswordNumberValidator, PasswordUpperValidator } from '../login/Restricted-login.directive';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit,AfterViewInit {
  isFirstnameTouched = false;
  isLastnameTouched = false;
  user:User;
  formregister:FormGroup;
  date;
private client_Id=environment.clientId;
 constructor(private router: Router,
  public renderer: Renderer2,
  private _ngZone: NgZone,
  private service: AuthService,
 // private cdr: ChangeDetectorRef
){

 }
  ngAfterViewInit(): void {
    // this.loadGoogleLibrary();
    // this.cdr.detectChanges();

  }

 FormFirst(){
  const passwordControl = new FormControl('', [
    Validators.required,
    PasswordLengthValidator(),
    PasswordUpperValidator(),
    PasswordNumberValidator()
  ]);
  this.formregister = new FormGroup({
    firstname: new FormControl('', [Validators.required]),
    lastname: new FormControl('', [Validators.required]),
    email: new FormControl('', Validators.email),
    password:passwordControl,
    confpassword: new FormControl('', [
      Validators.required,
      PasswordMatch(passwordControl)
    ]),
    gender: new FormControl('', Validators.required),
    dob:new FormControl('',DateOfBirthValidator())
  });
}
 ngOnInit(): void {
  this.FormFirst();


  const cssFilePaths = ['assets/theme/indigo/theme-light.css',
        'assets/layout/css/layout-light.css'];

  // Xóa các liên kết CSS hiện có trong document.head
  const existingLinks = document.head.querySelectorAll('link[rel="stylesheet"]');
  existingLinks.forEach((link: HTMLLinkElement) => {
      document.head.removeChild(link);
  });

  cssFilePaths.forEach(link => {
    const cssLink = this.renderer.createElement('link');
    this.renderer.setAttribute(cssLink, 'rel', 'stylesheet');
    this.renderer.setAttribute(cssLink, 'type', 'text/css');
    this.renderer.setAttribute(cssLink, 'href', link);
    this.renderer.appendChild(document.head, cssLink);
  });
}
showFirstnameError() {
  const firstnameControl = this.formregister.get('firstname');
  if (firstnameControl.value === '' && firstnameControl.touched) {
    console.log('Firstname is required!');
  }
}
showLastnameError() {
  const lastnameControl = this.formregister.get('lastname');
  if (lastnameControl.value === '' && lastnameControl.touched) {
    console.log('Lastname is required!');
  }
}
async onSubmit() {
  //this.formSubmitAttempt = false;

}



}
