import { AbstractControl,ValidatorFn } from "@angular/forms";


export function FirtNameValidator():ValidatorFn
{
return(control:AbstractControl):{[key: string]:boolean}|null=>{
if(control.value.trim()=="hi"){
  return{'OK':true};
}
return null;
};
}
export function PasswordMatch(passwordControl: AbstractControl): ValidatorFn {
  return (confirmPasswordControl: AbstractControl): { [key: string]: boolean } | null => {
    const password = passwordControl.value;
    const confirmPassword = confirmPasswordControl.value;

    if (password !== confirmPassword) {
      return { 'passwordMismatch': true };
    }

    return null;
  };
}
export function PasswordLengthValidator(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: boolean } | null => {
    if (control.value && !hasMinimumLength(control.value, 8)) {
      return { 'invalidPasswordLength': true };
    }
    return null;
  };
}

export function PasswordUpperValidator(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: boolean } | null => {
    if (control.value && !hasUppercaseLetter(control.value)) {
      return { 'missingUppercaseLetter': true };
    }
    return null;
  };
}

function hasMinimumLength(value: string, minLength: number): boolean {
  return value.length >= minLength;
}

function hasUppercaseLetter(value: string): boolean {
  const uppercaseRegex = /[A-Z]/;
  return uppercaseRegex.test(value);
}
export function PasswordNumberValidator(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: boolean } | null => {
    if (control.value && !hasNumber(control.value)) {
      return { 'missingNumber': true };
    }
    return null;
  };
}
function hasNumber(value: string): boolean {
  const numberRegex = /\d/;
  return numberRegex.test(value);
}


export function DateOfBirthValidator(): ValidatorFn {
  return (control: AbstractControl): { [key: string]: boolean } | null => {
    if (control.value && !isOver18YearsOld(control.value)) {
      return { 'ageRestriction': true };
    }
    return null;
  };
}

function isOver18YearsOld(dateOfBirth: string): boolean {
  const today = new Date();
  const birthDate = new Date(dateOfBirth);
  const yearsDiff = today.getFullYear() - birthDate.getFullYear();
  const monthsDiff = today.getMonth() - birthDate.getMonth();
  const daysDiff = today.getDate() - birthDate.getDate();

  if (yearsDiff > 18) {
    return true;
  } else if (yearsDiff === 18) {
    if (monthsDiff > 0) {
      return true;
    } else if (monthsDiff === 0 && daysDiff >= 0) {
      return true;
    }
  }

  return false;
}
