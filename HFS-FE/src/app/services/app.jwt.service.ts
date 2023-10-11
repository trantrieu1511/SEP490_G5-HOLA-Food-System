import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable()
export class JwtService {
  constructor(private jwtHelper: JwtHelperService) {}

  async getToken(): Promise<string> {
    return await this.jwtHelper.tokenGetter();
  }

  decodeToken(token: string): any {
    return this.jwtHelper.decodeToken(token);
  }
}