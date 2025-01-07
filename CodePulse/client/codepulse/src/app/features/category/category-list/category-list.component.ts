import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/category.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css']
})
export class CategoryListComponent implements OnInit {
categories!:Category[];
categories$ ?:Observable<Category[]>;

  
  constructor(private categoryService:CategoryService) {
    
  }
  ngOnInit(): void {
   this.categories$ = this.categoryService.GetAllCategories();
   
 
  }
  onSearch(query:string)
  {
    this.categories$ = this.categoryService.GetAllCategories(query);
  }
  sort(sortBy:string, sortDirection:string){
    this.categories$ = this.categoryService.GetAllCategories(undefined,sortBy,sortDirection);

  }
}
