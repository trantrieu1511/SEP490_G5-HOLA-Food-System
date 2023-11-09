export class PostModerator {
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
export class PostModeratorOutput {
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
export class PostModeratorInput{
  firstName: string;
  lastName: string;
  gender: string;
  birthDate: Date;
  email: string;
  phoneNumber: string;
  password:string;
}
