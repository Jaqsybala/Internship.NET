using System;
using System.Collections.Generic;
using Week2.DelegateTask;
using Xunit;

namespace UnitTests.Week2.DelegateTask.Tests
{
    public class DelegateTaskTests
    {
        public Dictionary<string, Func<Customer, double>> DiscountCalculation = new Dictionary<string, Func<Customer, double>>();

        [Theory]
        [InlineData("New", 100000, 0)]
        [InlineData("PermanentLargeOrders", 100001, 0.15)]
        [InlineData("PermanentLargeOrders", 100000, 0.1)]
        [InlineData("PermanentSmallOrders", 100000, 0.05)]
        public void DependsOn_CustomerType_Returns_Discount(string customerType, int orderSize, double expected)
        {
            Random rand = new Random();
            var orders = new List<Order>();
            for (int i = 0; i < orderSize; i++)
            {

                orders.Add(new Order()
                {
                    OrderNumber = i,
                    Amount = i + rand.Next(1, 10),
                    Date = DateTime.Now
                });
            }

            Customer customer = new Customer()
            {
                FullName = "Aziz",
                CustomerType = customerType,
                Address = "Almaty",
                Orders = orders,
            };

            DiscountCalculation.Add("New", (customer) => 0);
            DiscountCalculation.Add("PermanentLargeOrders", (customer) => customer.Orders.Count > 100000 ? 0.15 : 0.1);
            DiscountCalculation.Add("PermanentSmallOrders", (customer) => 0.05);

            SimpleCustomerFactory customerFactory = new SimpleCustomerFactory(DiscountCalculation);

            double discount = customerFactory.CalculateDiscount(customer);

            Assert.Equal(expected, discount);
        }

        [Theory]
        [InlineData("New", 100000, 0)]
        [InlineData("PermanentLargeOrders", 100001, 0.15)]
        [InlineData("PermanentLargeOrders", 100000, 0.1)]
        [InlineData("PermanentSmallOrders", 100000, 0.05)]
        [InlineData("VIP", 100000, 0.5)]
        public void Add_CustomerType_Returns_New_Discount(string customerType, int orderSize, double expected)
        {
            Random rand = new Random();
            var orders = new List<Order>();
            for (int i = 0; i < orderSize; i++)
            {
                orders.Add(new Order()
                {
                    OrderNumber = i,
                    Amount = i + rand.Next(1, 10),
                    Date = DateTime.Now
                });
            }

            Customer customer = new Customer()
            {
                FullName = "Aziz",
                CustomerType = customerType,
                Address = "Almaty",
                Orders = orders,
            };

            DiscountCalculation.Add("New", (customer) => 0);
            DiscountCalculation.Add("PermanentLargeOrders", (customer) => customer.Orders.Count > 100000 ? 0.15 : 0.1);
            DiscountCalculation.Add("PermanentSmallOrders", (customer) => 0.05);
            DiscountCalculation.Add("VIP", (customer) => 0.5);

            SimpleCustomerFactory customerFactory = new SimpleCustomerFactory(DiscountCalculation);

            double discount = customerFactory.CalculateDiscount(customer);

            Assert.Equal(expected, discount);
        }
    }
 }



