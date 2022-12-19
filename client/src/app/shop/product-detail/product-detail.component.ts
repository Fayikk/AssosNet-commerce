import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BasketService } from 'src/app/basket/basket.service';
import { IProduct } from 'src/app/shared/models/products';
import { BreadcrumbService } from 'xng-breadcrumb';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.css']
})
export class ProductDetailComponent implements OnInit {

  product:IProduct
  quantity=1;

  constructor(
      private shopService:ShopService,
      private activatedRoute:ActivatedRoute,
      private bcService:BreadcrumbService,
      private basketService:BasketService,
      private toastr:ToastrService) {
      this.bcService.set('@productDetails','');
     }

  ngOnInit(){
    this.loadProduct()
  }

  addItemToCard(){
    this.basketService.addItemToBasket(this.product,this.quantity);
  }

  incrementQuantity(){
    this.quantity++
  }
  decrementQuantity(){

    if(this.quantity <= 0){
      this.toastr.error("ooops!")
    }else{
    this.quantity--

    }
  }

  loadProduct(){
    this.shopService.getProduct(+this.activatedRoute.snapshot.paramMap.get('id')).subscribe(product =>{
      this.product = product;
      this.bcService.set('@productDetails',product.name);
    },
      error => {
        console.log(error)
      } 
    ) 
  }

}
