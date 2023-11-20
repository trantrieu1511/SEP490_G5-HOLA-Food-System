export class MenuModerator {
  modId:string;
  firstName: string;
  lastName: string;
  gender: string;
  birthDate: Date;
  email: string;
  phoneNumber: string;
  password:string;
  confirmpassword:string;
}
export class MenuModeratorOutput {
  modId:string;
  firstName: string;
  lastName: string;
  gender: string;
  birthDate: Date;
  email: string;
  phoneNumber: string;
  avatar: string;
  isOnline: boolean;
  isBanned: boolean;
}
export class MenuModeratorInput{
  firstName: string;
  lastName: string;
  gender: string;
  birthDate: Date;
  email: string;
  phoneNumber: string;
  password:string;
}
