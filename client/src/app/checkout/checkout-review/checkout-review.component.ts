import { CdkStepper } from '@angular/cdk/stepper';
import { Component, Input, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { BasketService } from 'src/app/basket/basket.service';
import { IBasket } from 'src/app/shared/models/basket';

@Component({
  selector: 'app-checkout-review',
  templateUrl: './checkout-review.component.html',
  styleUrls: ['./checkout-review.component.css']
})
export class CheckoutReviewComponent implements OnInit {
  basket$:Observable<IBasket>
  @Input() appStepper: CdkStepper

  constructor(private basketService:BasketService,private toastr:ToastrService) { }

  ngOnInit(): void {
  }

  createPaymentIntent(){
    return this.basketService.createPaymentIntent().subscribe((response:any) => {
      this.toastr.success("Payment intent created");
      this.appStepper.next();
    },error => {
      console.log(error);
      this.toastr.error(error.message);
    })
  }

}
