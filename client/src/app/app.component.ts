import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./Layouts/header/header.component";
import { HttpClient } from '@angular/common/http';
import { Product } from './Shared/models/product';
import { pagination } from './Shared/models/pagination';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, HeaderComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
baseUrl = 'https://localhost:5001/api/';
private http = inject(HttpClient);
  title = 'First Hands On';
  products:Product[]=[];
  ngOnInit(): void {
    this.http.get<pagination<Product>>(this.baseUrl + 'products').subscribe({
      next : response => this.products = response.data,
      error: error => console.error(error),
      complete:()=>console.log('complete')
      
    })
  }

}
