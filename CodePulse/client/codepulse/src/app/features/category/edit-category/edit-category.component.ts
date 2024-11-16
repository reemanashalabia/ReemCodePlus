import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { CategoryService } from '../services/category.service';
import { Category } from '../models/category.model';
import { UpdateCategoryRequest } from '../models/update-category-request.model';

@Component({
  selector: 'app-edit-category',
  templateUrl: './edit-category.component.html',
  styleUrls: ['./edit-category.component.css']
})
export class EditCategoryComponent implements OnInit,OnDestroy {
  id:string | null = null;
  category?:Category;
  paramsSubscibtion! : Subscription;
  editCategorySubscibtion! : Subscription;
  deleteCategorySubscibtion! : Subscription;

/**
 *
 */
constructor(private route:ActivatedRoute,private categoryService : CategoryService,private router : Router) {
  
}
onFormSubmit():void{
console.log(this.category);
const updateCategory : UpdateCategoryRequest = {
  name : this.category?.name ?? '',
  urlHandle : this.category?.urlHandle ?? ''
}; 
// pass object to service 
if(this.id)
  {
  this.editCategorySubscibtion =  this.categoryService.updateCategory(this.id , updateCategory).subscribe({
      next:(response)=>{
          this.router.navigateByUrl('/admin/categories')
      }
    })

  }
}
onDelete():void{
  if(this.id)
  {
    this.deleteCategorySubscibtion =  this.categoryService.deleteCategory(this.id ).subscribe({
      next:(response)=>{
          this.router.navigateByUrl('/admin/categories')
      }
    })
  }
}
  ngOnDestroy(): void {
      this.paramsSubscibtion.unsubscribe();
      this.editCategorySubscibtion.unsubscribe();
      this.deleteCategorySubscibtion.unsubscribe();

      
  }
  ngOnInit(): void {
   this.paramsSubscibtion =   this.route.paramMap.subscribe({
      next:(params)=>{
        this.id =params.get('id');
        if(this.id)
          {
            // get data from the API for this category id[

           
            this.categoryService.GetCategoryById(this.id).subscribe({
              next:(value)=> {
                this.category= value;
              },
            });
          }
      }
    });
  }
}
