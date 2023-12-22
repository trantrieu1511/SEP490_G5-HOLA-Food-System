import { forwardRef, inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { iFunction } from 'src/app/modules/shared-module/shared-module';
import { JwtService } from '../app.jwt.service';
import { RoleNames } from 'src/app/utils/roleName';

export const bounaryGuard: CanActivateFn = (route, state) => {
  debugger
  const _iFunction = inject(forwardRef(() => iFunction));
  const router = inject(forwardRef(() => Router));
  const jwtService = inject(forwardRef(() => JwtService));

  const requiredRole = route.data['requiredRole'];
  const storedUser = localStorage.getItem("user");

  if(!storedUser && requiredRole.includes('Guest')){
    return true;
  }

  var refreshToken = _iFunction.getCookie('refreshToken')

  if(!refreshToken){
    localStorage.removeItem('user');
    jwtService.removeToken();
    router.navigate(['/login']);
    return false;
  }

  
  if (!storedUser) {
    localStorage.removeItem('user');
    jwtService.removeToken();
    router.navigate(['/login']);
    return false;
  }

  // Parse the stored user object from JSON
  const user = JSON.parse(storedUser);
  const roleName = RoleNames[user.role];

  if (requiredRole.includes(roleName)) {
    return true; // user has access
  } else {
    if (state.url === "/" || state.url === "/homepage") {
      // Handle the case where the route URL is "/"
      router.navigate(['/HFSBusiness']);
      return false;
    }else{
      router.navigate(['/accessdeny']);
      return false;
    }
  }
  //return true;
};
