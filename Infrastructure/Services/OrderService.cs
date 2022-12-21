using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> orderRepo;
        private readonly IGenericRepository<DeliveryMethod> dmRepo;
        private readonly IGenericRepository<Product> productRepo;
        private readonly IBasketRepository basketRepo;

        public OrderService(IGenericRepository<Order> orderRepo, IGenericRepository<DeliveryMethod> dmRepo,IGenericRepository<Product> productRepo,IBasketRepository basketRepo){
            this.orderRepo = orderRepo;
            this.dmRepo = dmRepo;
            this.productRepo = productRepo;
            this.basketRepo = basketRepo;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            //get basket from the repo
            var basket = await basketRepo.GetBasketAsync(basketId);
            //get items from the repo
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productItem = await productRepo.GetByIdAsync(item.Id);
                var itemOrdered = new ProductItemOrdered(productItem.Id,productItem.Name,
                productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrdered,productItem.Price,item.Quantity);
                items.Add(orderItem);
            }
            //get delivery method
            var deliveryMethod = await dmRepo.GetByIdAsync(deliveryMethodId);
            //calc subtotal
            var subtotal = items.Sum(items => items.Price * items.Quantity);

            //create order
            var order = new Order(items,buyerEmail,shippingAddress,deliveryMethod,subtotal);
            //Todo: save to db

            //return order
            return order;

        
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new System.NotImplementedException();
        }
    }
}