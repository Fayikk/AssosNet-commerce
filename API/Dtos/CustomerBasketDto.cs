using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }      
        public int? DeliveryMethodId { get; set; }
        public string  ClientSecret { get; set; } 
        public decimal ShippingPrice { get; set; }         
    }
}