import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket/basket.service';
import { IPagination } from './shared/models/pagination';
import { IProduct } from './shared/models/products';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'Client';
  products:IProduct[] | undefined
  constructor(private http:HttpClient,private basketService:BasketService){}

  ngOnInit(): void {
    const basketId = localStorage.getItem('basket_id');//comming localstorage for this method object
    if(basketId){
      this.basketService.getBasket(basketId).subscribe(() => {
        console.log("Initialize Basket")
      },error => console.log(error)
      )
    }
  }
}
