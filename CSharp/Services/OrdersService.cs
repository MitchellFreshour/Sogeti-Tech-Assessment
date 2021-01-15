using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSharp.Services
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public List<int> ProductIds { get; set; }
    }

    public interface IOrdersService
    {
        int CreateOrder(int CustomerId, List<int> ProductIds);

        int UpdateOrder(int OrderId, List<int> ProductIds);

        int CancelOrder(int OrderId);

        List<int> ListOrders(int CustomerId);
    }

    public class OrdersService : IOrdersService
    {
        // This is a substitue to an actual database, and I cannot us
        private IList<OrderModel> orders;

        private int Failure = -1;

        public OrdersService()
        {
            orders = new List<OrderModel>();
        }

        public int CreateOrder(int CustomerId, List<int> ProductIds)
        {
            try
            {
                var orderId = orders.Count + 1;
                orders.Add(new OrderModel
                {
                    OrderId = orderId,
                    CustomerId = CustomerId,
                    ProductIds = ProductIds
                });
                return orderId;
            }
            catch (Exception e)
            {
                return Failure;
            }
        }

        public int UpdateOrder(int OrderId, List<int> ProductIds)
        {
            var order = orders.First(order => order.OrderId == OrderId);
            if (order != null)
            {
                order.ProductIds = ProductIds;

                return order.OrderId;
            }

            return Failure;
        }

        public int CancelOrder(int OrderId)
        {
            var orderToDelete = orders.First(order => order.OrderId == OrderId);
            if (orderToDelete != null)
            {
                orders.Remove(orderToDelete);
                return orderToDelete.OrderId;
            }
            return Failure;
        }

        public List<int> ListOrders(int CustomerId)
        {
            return orders
                .Where(order => order.CustomerId == CustomerId)
                .SelectMany(order => order.ProductIds)
                .ToList();
        }
    }
}