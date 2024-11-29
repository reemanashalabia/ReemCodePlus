import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { BlogPostService } from '../services/blog-post.service';
import { BlogPost } from '../models/blog-post.model';
import { CategoryService } from '../../category/services/category.service';
import { Category } from '../../category/models/category.model';
import { EditBlogPost } from '../models/edit-blog-post.model';

@Component({
  selector: 'app-edit-blogpost',
  templateUrl: './edit-blogpost.component.html',
  styleUrls: ['./edit-blogpost.component.css']
})
export class EditBlogpostComponent implements OnInit , OnDestroy {
  id:string | null = null;
  routeSubscription! : Subscription;
  model? : BlogPost ;
  blogPostServiceSubscription! : Subscription;
  blogPostServiceDeleteSubscription! : Subscription;
  blogPostServiceGetSubscription! : Subscription;

  categories$? :Observable<Category[]> ;
  selectedCategories? :string[];

  /**
   *inject Activated Route to get Id from route and service to get data
   */
  constructor(private route:ActivatedRoute ,private blogPostService : BlogPostService , private categoryService : CategoryService, private router : Router ) {
   

  }
  ngOnDestroy(): void {
    this.routeSubscription?.unsubscribe();  
    this.blogPostServiceGetSubscription?.unsubscribe();  
    this.blogPostServiceDeleteSubscription?.unsubscribe();  

    this.blogPostServiceSubscription.unsubscribe();}
  ngOnInit(): void {
    this.routeSubscription =this.route.paramMap.subscribe({
      next:(params) =>{
      this.id =  params.get('id');
      // Get Blog Post From APi
      if(this.id)
        {
        this.blogPostServiceGetSubscription =  this.blogPostService.getBlogPostById(this.id).subscribe({
            next:(response)=>{
              this.model = response;
              this.selectedCategories = response.categories.map(x=>x.id);
            }
          });

        }
      },
    });
    this.categories$ = this.categoryService.GetAllCategories();
  }
  onSubmitForm(){
    if(this.model && this.id)
      {
        var updatedBlogPost : EditBlogPost ={
          title : this.model?.title,
          author : this.model?.author,
          content : this.model?.content,
          categories :this.selectedCategories ?? [],
          featuredImageUrl : this.model?.featuredImageUrl,
          isVisible : this.model?.isVisible,
          publishedDate : this.model?.publishedDate,
          shortDescription : this.model?.shortDescription,

          urlHandle : this.model?.urlHandle,



        }
        this.blogPostServiceSubscription = this.blogPostService.updateBlogPost(this.id , updatedBlogPost).subscribe({
          next:(response) =>{
            this.router.navigateByUrl('/admin/blogposts')
          }
        });
      }
  }
  OnDelete():void{
    if(this.id)
      {
        this.blogPostServiceDeleteSubscription = this.blogPostService.deleteBlogPost(this.id).subscribe({
          next:()=>{
            this.router.navigateByUrl("/admin/blogposts")
          }
        })
      }

  }
}
