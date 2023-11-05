import { CanActivateFn, Router } from '@angular/router';
import { JwtService } from '../app.jwt.service';
import { inject } from '@angular/core';
import { RoleNames } from '../../utils/roleName';

export const authGuard: CanActivateFn = async (route, state) => {
  const router = inject(Router);
  //const jwtService = inject(JwtService);
  var token = sessionStorage.getItem('JWT');
  var role = sessionStorage.getItem('role');
  const timetoken = sessionStorage.getItem('timetoken');
  //var token = await jwtService.getToken();

  if (token == null) {
    router.navigate(['/login']);
    return false;
  }
  const timetoken1 = Number(timetoken);
  const currentTime = Date.now() / 1000; // Đổi sang giây

  // Kiểm tra thời gian hết hạn của token

  // get data from Resolver
  const requiredRole = route.data['requiredRole'];
  // // get role from jwt
  // const decodedToken = jwtService.decodeToken(token);
  // const role = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
  //convert to role name
  const roleName = RoleNames[role];
  if (timetoken1 < currentTime) {
    router.navigate(['/login']);
  }

  if (requiredRole.includes(roleName)) {
    return true; // user has access
  } else {
    router.navigate(['/accessdeny']);
    return false;
  }
};
