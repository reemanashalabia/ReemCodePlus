import { Injectable } from '@angular/core';
import { LoginRequest } from '../models/login-request.model';
import { BehaviorSubject, Observable } from 'rxjs';
import { LoginResponse } from '../models/login-response.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';
import { user } from '../models/user.model';
import { CookieService } from 'ngx-cookie-service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  // create behavior subject eith initial value undifined
  $user = new BehaviorSubject<user |undefined>(undefined); 

  constructor(private http:HttpClient, private cookieService:CookieService) { }
  login( request:LoginRequest):Observable<LoginResponse>{
    return this.http.post<LoginResponse>(`${environment.apiBaseUrl}api/auth/login`,{
      email:request.email,
      password:request.password
    });
  }
  setUser(user:user):void{
    // we will sending the new user to any subscriber of this behaviour subject
    this.$user.next(user);
    localStorage.setItem('user-email',user.email)
    localStorage.setItem('user-roles',user.roles.join(','));

  }
  user():Observable<user| undefined>{
return this.$user.asObservable();
  }
  logout():void{
    localStorage.clear();
    this.cookieService.delete('Authorization','/');
    this.$user.next(undefined);

      }
      getUser():user|undefined{
        const email = localStorage.getItem('user-email');
        const roles = localStorage.getItem('user-roles');
        if(email && roles )
          {
            const user:user={
              email : email,
              roles:roles?.split(',')
            };
            return user;
          }
          return undefined; 

      }
}
