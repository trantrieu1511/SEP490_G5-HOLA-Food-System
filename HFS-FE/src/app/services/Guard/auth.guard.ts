import { CanActivateFn, Router } from '@angular/router';
import { JwtService } from '../app.jwt.service';
import { inject } from "@angular/core"
import { RoleNames } from '../../utils/roleName';


export const authGuard:  CanActivateFn = async (route, state) => {
  
  const router = inject(Router);
  const jwtService = inject(JwtService);

  var token = await jwtService.getToken();
  
  if(token == null){
    router.navigate(['/login']);
    return false;
  }

   // get data from Resolver
  const requiredRole = route.data['requiredRole'];
  // get role from jwt
  const decodedToken = jwtService.decodeToken(token);
  const role = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
  //convert to role name
  const roleName = RoleNames[role];


  if (roleName === requiredRole) {
    return true; // user has access
  } else {    
    router.navigate(['/accessdeny']); 
    return false;
  }
};
