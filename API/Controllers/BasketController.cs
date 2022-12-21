using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basket;
        private readonly IMapper _mapper;
        public BasketController(IBasketRepository basket,IMapper mapper)
        {
            _mapper = mapper;
            _basket = basket;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketId(string id){
            var basket  = await _basket.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]

        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basket){
            
            var CustomerBasket = _mapper.Map<CustomerBasketDto,CustomerBasket>(basket);

            var updateBasket = await _basket.UpdateBasketAsync(CustomerBasket);
            return Ok(updateBasket);

        }

        [HttpDelete]
        public async Task<ActionResult<CustomerBasket>> DeleteBasket(string id){
            var deleteBasket = await _basket.DeleteBasketAsync(id);
            return Ok();
        }
    }
}