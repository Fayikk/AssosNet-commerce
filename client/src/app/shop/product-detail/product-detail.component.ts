import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
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

  constructor(private shopService:ShopService,private activatedRoute:ActivatedRoute,
    private bcService:BreadcrumbService) {
      this.bcService.set('@productDetails','');
     }

  ngOnInit(){
    this.loadProduct()
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
