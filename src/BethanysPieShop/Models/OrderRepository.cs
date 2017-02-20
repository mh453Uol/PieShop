using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPieShop.Models
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ShoppingCart _shoppingCart;

        public OrderRepository(AppDbContext appDbContext, ShoppingCart shoppingCart)
        {
            _appDbContext = appDbContext;
            _shoppingCart = shoppingCart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;
            _appDbContext.Orders.Add(order);

            var shoppingCartItems = _shoppingCart.ShoppingCartItems;
            foreach(var item in shoppingCartItems)
            {
                var orderDetails = new OrderDetail()
                {
                    Amount = item.Amount,
                    PieId = item.Pie.PieId,
                    OrderId = order.OrderId,
                    Price = item.Pie.Price
                };

                _appDbContext.OrderDetails.Add(orderDetails);
            }

            _appDbContext.SaveChanges();
        }
    }
}
