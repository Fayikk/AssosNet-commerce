import { Component, Input, OnInit } from '@angular/core';
import { IProduct } from 'src/app/shared/models/products';



@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.css']
})
export class ProductItemComponent implements OnInit {
  @Input() product:IProduct

  constructor() { }

  ngOnInit(): void {
  }

}
