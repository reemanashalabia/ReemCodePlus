import { Component, OnDestroy } from '@angular/core';
import { AddCategoryRequest } from '../models/add-category-request.model';
import { CategoryService } from '../services/category.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-add-category',
  templateUrl: './add-category.component.html',
  styleUrls: ['./add-category.component.css']
})
export class AddCategoryComponent implements OnDestroy {
  model:AddCategoryRequest;
  private addCategorySubscription? : Subscription; // subscription type or undifnrd
  constructor(private categoriesService:CategoryService){
    this.model = {
      name:'',
      urlHandle:''
    };
  }
  ngOnDestroy(): void {
    // throw new Error('Method not implemented.');
    this.addCategorySubscription?.unsubscribe();
  }
  onFormSubmit(){
    console.log(this.model)
this.addCategorySubscription = this.categoriesService.addCategory(this.model).subscribe({
  next:(response)=>{
console.log(response)
  },
  error:()=>{

  }
});  
}
}
