import { Component, OnDestroy } from '@angular/core';
import { AddBlogPost } from '../models/add-blog-post.model';
import { BlogPostService } from '../services/blog-post.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-blogpost',
  templateUrl: './add-blogpost.component.html',
  styleUrls: ['./add-blogpost.component.css']
})
export class AddBlogpostComponent  implements OnDestroy{
model:AddBlogPost;
blogPostSubscription! : Subscription;
constructor(private blogPostService:BlogPostService, private router:Router){
  this.model = {
    title:'',
    shortDescription : '',
    content: '',
    featuredImageUrl: '',
    author:'',
    isVisible : true,
    publishedDate:new Date(),
    urlHandle: ''
  }
}
  ngOnDestroy(): void {
    this.blogPostSubscription.unsubscribe()
  }
onSubmitForm():void{
  console.log(this.model)
 this.blogPostSubscription = this.blogPostService.createBlogPost(this.model).subscribe({
    next:(response)=>{
      this.router.navigateByUrl('/admin/blogposts');
    }
  })

}
}
