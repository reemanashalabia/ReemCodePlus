import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { AuthService } from './services/auth.service';
import { jwtDecode } from 'jwt-decode';

export const authGuard: CanActivateFn = (route, state) => {

  const cookieService=inject(CookieService);
  const authService=inject(AuthService);
  const router=inject(Router);
  const user=authService.getUser();

  // check for token jwt
  let token = cookieService.get("Authorization");
if(token && user){
  // check if not expired
token = token.replace('Bearer','')
const decodedToken:any = jwtDecode(token);
const expirationDate = decodedToken.exp * 1000;
const currentTime = new Date().getTime();
if(expirationDate < currentTime)
  {
     // logout
  authService.logout();
  // return to login page
  return router.createUrlTree(['/login',{queryParams:{returnUrl:state.url}}]);
  }
  else{
    // check for user role
    if(user.roles.includes("Writer"))
      {
        return true;
      }
      else 
      {
        alert("UnAuthorized");
        return false;
      }
  }

}
else{
  // logout
  authService.logout();
  // return to login page
  return router.createUrlTree(['/login',{queryParams:{returnUrl:state.url}}]);
}





};
