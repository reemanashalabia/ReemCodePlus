import { Component, OnDestroy, OnInit } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-post.model';
import { BlogPostService } from '../services/blog-post.service';
import { Observable, Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/models/category.model';
import { ImageService } from 'src/app/shared/components/image-selector/image.service';

@Component({
  selector: 'app-add-blogpost',
  templateUrl: './add-blogpost.component.html',
  styleUrls: ['./add-blogpost.component.css']
})
export class AddBlogpostComponent  implements OnInit, OnDestroy{
model:AddBlogPost;
blogPostSubscription! : Subscription;
imageSelectSubscription! : Subscription;

categories$!:Observable<Category[]>;
isImageSelectorVisible:boolean = false;

constructor(private blogPostService:BlogPostService,private imageService:ImageService, private router:Router,private categoryService :CategoryService){
  this.model = {
    title:'',
    shortDescription : '',
    content: '',
    featuredImageUrl: '',
    author:'',
    isVisible : true,
    publishedDate:new Date(),
    urlHandle: '',
    categories : []
  }
}
  ngOnInit(): void {
   this.categories$ = this.categoryService.GetAllCategories() 
   this.imageSelectSubscription= this.imageService.onSelectImage().subscribe({
    next:(response)=>{
      if(this.model )
        {
          this.model.featuredImageUrl = response.url;
          this.isImageSelectorVisible = false;

        }

    }
  }) }
  ngOnDestroy(): void {
    this.blogPostSubscription.unsubscribe()
    this.imageSelectSubscription?.unsubscribe();

  }
onSubmitForm():void{
  console.log(this.model)
 this.blogPostSubscription = this.blogPostService.createBlogPost(this.model).subscribe({
    next:(response)=>{
      this.router.navigateByUrl('/admin/blogposts');
    }
  })

}
openImageSelector():void{
  this.isImageSelectorVisible = true;
    }
    closeImageSelector():void{
      this.isImageSelectorVisible = false;
  
    }
}
