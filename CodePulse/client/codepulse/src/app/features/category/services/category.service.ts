import { Injectable } from '@angular/core';
import { AddCategoryRequest } from '../models/add-category-request.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Category } from '../models/category.model';
import { environment } from 'src/environments/environment.development';
import { UpdateCategoryRequest } from '../models/update-category-request.model';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http:HttpClient) { }
  addCategory(model:AddCategoryRequest):Observable<void>{
    return this.http.post<void>(`${environment.apiBaseUrl}api/Categories`,model);
  }
  GetAllCategories():Observable<Category[]>{
    return this.http.get<Category[]>(`${environment.apiBaseUrl}api/Categories`)
  }
  GetCategoryById(id:string):Observable<Category>{
return this.http.get<Category>(`${environment.apiBaseUrl}api/Categories/${id}`)
  }
  updateCategory(id:string , request : UpdateCategoryRequest):Observable<Category>
  {
    return this.http.put<Category>(`${environment.apiBaseUrl}api/Categories/${id}`,request);

  }
  deleteCategory(id:string):Observable<Category>{
    return this.http.delete<Category>(`${environment.apiBaseUrl}api/Categories/${id}`)
      }
}
 