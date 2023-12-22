import { CanActivateFn, Router } from '@angular/router';
import { JwtService } from '../app.jwt.service';
import { inject } from '@angular/core';
import { RoleNames } from '../../utils/roleName';
import { iFunction } from 'src/app/modules/shared-module/shared-module';
import { AuthService } from '../auth.service';

export const authGuard: CanActivateFn = async (route, state) => {
  debugger
  const router = inject(Router);
  const jwtService = inject(JwtService);
  const _iFunction = inject(iFunction);
  const authService = inject(AuthService)
  //var token = sessionStorage.getItem('JWT');
  //var role = sessionStorage.getItem('role');
  const requiredRole = route.data['requiredRole'];
  if(authService.getUserInfor() == null && requiredRole.includes('Guest')){
    return true;
  }

  var refreshToken = _iFunction.getCookie('refreshToken')

  if(!refreshToken){
    localStorage.removeItem('user');
    jwtService.removeToken();
    router.navigate(['/login']);
    return false;
  }

  var token = _iFunction.getCookie('token')
  //const timetoken = sessionStorage.getItem('timetoken');
  //var token = await jwtService.getToken();
  
  if (!token) {
    
    const isRefreshSuccess = await jwtService.tryRefreshingTokens(); 
    if (!isRefreshSuccess) { 
      
      localStorage.removeItem('user');
      jwtService.removeToken();
      router.navigate(['/login']);
      return false;
    }
  }

  var role = authService.getUserInfor().role;

  // const timetoken1 = Number(timetoken);
  // const currentTime = Date.now() / 1000; // Đổi sang giây

  // Kiểm tra thời gian hết hạn của token

  // get data from Resolver
  
  // // get role from jwt

  //convert to role name
  const roleName = RoleNames[role];
  // if (timetoken1 < currentTime) {
  //   router.navigate(['/login']);
  // }

  if (requiredRole.includes(roleName)) {
    return true; // user has access
  } else {
    router.navigate(['/accessdeny']);
    return false;
  }
};

