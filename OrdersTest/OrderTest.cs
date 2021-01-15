using System;
using Xunit;
using CSharp.Services;
using System.Collections.Generic;

namespace OrdersTest
{
    public class OrderTest
    {
        private IOrdersService ordersService;

        private OrderModel Order1 = new OrderModel
        {
            CustomerId = 123,
            ProductIds = new List<int>() { 1, 2, 3 }
        };

        private OrderModel Order2 = new OrderModel
        {
            CustomerId = 456,
            ProductIds = new List<int>() { 4, 5, 6 }
        };

        private OrderModel Order3 = new OrderModel
        {
            CustomerId = 123,
            ProductIds = new List<int>() { 7, 8, 9 }
        };

        protected void SetUp()
        {
            ordersService = new OrdersService();
        }

        [Fact]
        public void AddAndRetrieveOne()
        {
            SetUp();

            var orderId = ordersService.CreateOrder(Order1.CustomerId, Order1.ProductIds);
            Assert.Equal(1, orderId);

            var customerProductIds = ordersService.ListOrders(Order1.CustomerId);
            Assert.Equal(customerProductIds, Order1.ProductIds);
        }

        [Fact]
        public void AddAndUpdate()
        {
            SetUp();

            var orderId = ordersService.CreateOrder(Order1.CustomerId, Order1.ProductIds);
            Assert.Equal(1, orderId);

            var originalProductIds = ordersService.ListOrders(Order1.CustomerId);
            Assert.Equal(originalProductIds, Order1.ProductIds);

            var newProductIds = new List<int>() { 10, 11, 12 };

            var updatedOrderId = ordersService.UpdateOrder(orderId, newProductIds);
            Assert.Equal(1, updatedOrderId);

            var updatedProductIds = ordersService.ListOrders(Order1.CustomerId);
            Assert.Equal(newProductIds, updatedProductIds);
        }

        [Fact]
        public void AddAndRetrieveMultiple()
        {
            SetUp();

            var orderList = new List<OrderModel>() { Order1, Order2, Order3 };
            var expectedOrderId = 1;

            foreach (var order in orderList)
            {
                var orderId = ordersService.CreateOrder(order.CustomerId, order.ProductIds);
                Assert.Equal(expectedOrderId, orderId);
                expectedOrderId++;
            }

            var customerProductIds = ordersService.ListOrders(Order1.CustomerId);

            var expectedProductIds = Order1.ProductIds;
            expectedProductIds.AddRange(Order3.ProductIds);

            Assert.Equal(customerProductIds, expectedProductIds);
        }

        [Fact]
        public void RetrieveNone()
        {
            SetUp();

            var customerProductIds = ordersService.ListOrders(Order1.CustomerId);
            Assert.Empty(customerProductIds);
        }

        [Fact]
        public void AddMultipleThenDeleteOne()
        {
            SetUp();

            var orderList = new List<OrderModel>() { Order1, Order2, Order3 };
            var expectedOrderId = 1;

            foreach (var order in orderList)
            {
                var orderId = ordersService.CreateOrder(order.CustomerId, order.ProductIds);
                expectedOrderId++;
            }

            var deletedId = ordersService.CancelOrder(3);
            Assert.Equal(3, deletedId);
        }
    }
}