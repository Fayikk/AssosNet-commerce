import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IBrand } from 'src/app/shared/models/brand';
import { IProduct } from 'src/app/shared/models/products';
import { IType } from 'src/app/shared/models/productType';
import { ShopParams } from 'src/app/shared/models/shopParams';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.css']
})
export class ShopComponent implements OnInit {
  @ViewChild('search',{static: true}) searchTerm:ElementRef;
  products:IProduct[];
  brands:IBrand[]
  types:IType[]
  brandIdSelected = 0;
  typeIdSelected = 0;
  sortSelected = 'name';
  shopParams = new ShopParams()
  totalCount : number;

sortOptions = [
    {name: 'Alphabetical',value:'name'},
    {name: 'Price: Low to High',value:'priceAsc'},
    {name: 'Price: Hight to low', value:'priceDesc'}
  ]


  constructor(private shopService:ShopService) { }

  ngOnInit() {
   this.getProducts();
   this.getBrands();
   this.getTypes()
  }

  getProducts(){
    this.shopService.getProducts(this.shopParams).subscribe(response => {
      this.products = response.data;
      this.shopParams.pageNumber = response.pageIndex;
      this.shopParams.pageSize = response.pageSize;
      this.totalCount = response.count;
    },error => 
    console.log(error)
    )
  }

  getBrands(){
    this.shopService.getBrands().subscribe(response=>{
      this.brands = [{id:0,name:'All'},...response];
    },error =>{
      console.log(error);
    }
    )
  }

  getTypes(){
    this.shopService.getTypes().subscribe(response=>{
      this.types = [{id:0,name:'All'},...response];
    },error =>{
      console.log(error);
    }
    )
  }

  onBrandSelected(brandId:number){
    this.shopParams.brandId = brandId;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onTypeSelected(typeId:number){
    this.shopParams.typeId = typeId;
    this.shopParams.pageNumber = 1;

    this.getProducts();
  }

  onSortSelected(sort : string){
    this.shopParams.sort = sort;
    this.getProducts()
  }

  onPageChanged(event:any){
    if(this.shopParams.pageNumber !==event){
      this.shopParams.pageNumber=event;
      this.getProducts();
    }
   
  }

  onSearch(){
    this.shopParams.search = this.searchTerm.nativeElement.value;
    this.shopParams.pageNumber = 1;
    this.getProducts();
  }

  onReset(){
    this.searchTerm.nativeElement.value = "";
    this.shopParams = new ShopParams();
    this.getProducts();
  }
}
