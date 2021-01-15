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

        [HttpPost("create/{CustomerId}")]
        public string CreateOrder(int CustomerId, [FromBody] List<int> ProductIds)
        {
            var result = _orderService.CreateOrder(CustomerId, ProductIds);
            if (result != Failure)
            {
                return $"Created Order {result} Sucessfully";
            }

            return $"Failed to create Order";
        }

        [HttpPut("update/{OrderId}")]
        public string UpdateOrder(int OrderId, [FromBody] List<int> ProductIds)
        {
            var result = _orderService.UpdateOrder(OrderId, ProductIds);
            if (result != Failure)
            {
                return $"Updated Order {result} Sucessfully!";
            }

            return $"Failed to update Order {OrderId}";
        }

        [HttpDelete("delete/{OrderId}")]
        public string CancelOrder(int OrderId)
        {
            var result = _orderService.CancelOrder(OrderId);
            if (result != Failure)
            {
                return $"Cancelled Order {OrderId} Sucessfully!";
            }
            return $"Failed to cancel Order {OrderId}";
        }

        [HttpGet("list/{CustomerId}")]
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