using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CSharp.Controllers
{
    [ApiController]
    [Route("test")]
    public class OrdersController : Controller
    {
        private readonly IOrdersService _orderService;

        private readonly int Failure = -1;

        public OrdersController(
            IOrdersService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("create")]
        public string CreateOrder(int CustomerId, List<int> ProductIds)
        {
            var result = _orderService.CreateOrder(CustomerId, ProductIds);
            if (result != Failure)
            {
                return $"Created Order {result} Sucessfully";
            }

            return $"Failed to create Order";
        }

        [HttpPut("update")]
        public string UpdateOrder(int OrderId, List<int> ProductIds)
        {
            var result = _orderService.UpdateOrder(OrderId, ProductIds);
            if (result != Failure)
            {
                return $"Updated Order {result} Sucessfully!";
            }

            return $"Failed to update Order {OrderId}";
        }

        [HttpDelete("delete")]
        public string CancelOrder(int OrderId)
        {
            var result = _orderService.CancelOrder(OrderId);
            if (result != Failure)
            {
            }
            return $"Cancelled Order {OrderId} Sucessfully!";
        }

        [HttpGet("list")]
        public List<int> ListOrders(int CustomerId)
        {
            var result = _orderService.ListOrders(CustomerId);
            if (result.Count != 0)
            {
                return result;
            }
            return new List<int>();
        }
    }
}