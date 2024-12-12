import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { BlogImage } from '../models/blog-image.model';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class ImageService {
  selectedImage:BehaviorSubject<BlogImage> = new BehaviorSubject<BlogImage>({
    id:'',
    fileExtension : '',
    title:'',
    url:'',
    fileName : ''
  });

  constructor(private httpClient:HttpClient) { }
  UploadImage(file:File,fileName:string,title:string):Observable<BlogImage>{
      const formData = new FormData();
      formData.append('file',file);
      formData.append('fileName',fileName);
      formData.append('title',title);
      return this.httpClient.post<BlogImage>(`${environment.apiBaseUrl}api/images`,formData);
  }
  GetAllImages ():Observable<BlogImage[]>
  {
    return this.httpClient.get<BlogImage[]>(`${environment.apiBaseUrl}api/images`)
  }
  selectImage(image:BlogImage):void{
    this.selectedImage.next(image);
  }
  onSelectImage():Observable<BlogImage>{
    return this.selectedImage.asObservable();
  }
}
