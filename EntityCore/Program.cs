using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityCore
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new OrderContext())
            {
                //var p = new Product
                //{
                //    Title = "Basketball",
                //    Price = 8d
                //};
                //db.Products.Add(p);

                //var o = new Order
                //{
                //    OrderItems = new List<OrderItem>(),
                //    CustomerName = "CHRIS",
                //    OrderDate = DateTime.Now
                //};

                //var oi = new OrderItem();
                //oi.Order = o;
                //oi.Product = p;
                //oi.SalePrice = 7d;
                //oi.Quantity = 3;

                //o.OrderItems.Add(oi);

                //db.Orders.Add(o);

                //db.SaveChanges();



                //var orders = db.Orders
                //    .Include(order => order.OrderItems)
                //    .ThenInclude(orderItem => orderItem.Product);

                //foreach (var order in orders)
                //{
                //    Console.WriteLine($"{order.OrderDate} - {order.CustomerName}");

                //    foreach (var orderitem in order.OrderItems)
                //    {
                //        Console.WriteLine($" ({orderitem.Product.Title}) {orderitem.Quantity} * {orderitem.SalePrice}");
                //    }
                //}


                var p1 = new Product { Title = "Football", Price = 12d };
                db.Products.Add(p1);

                var p2 = new Product { Title = "Soccer Ball", Price = 4d };
                db.Products.Add(p2);

                var customer = new Customer { CustomerName = "Chris", StreetAddress = "123 SW 73rd pl" };
                db.Customers.Add(customer);

                var oi = new Order { Customer = customer, OrderDate = DateTime.Now, OrderItems = new List<OrderItem>() };

                var oi1 = new OrderItem { Order = oi, Product = p1, Quantity = 3, SalePrice = 10d };
                var oi2 = new OrderItem { Order = oi, Product = p2, Quantity = 1, SalePrice = 3d };

                oi.OrderItems.Add(oi1);
                oi.OrderItems.Add(oi2);
                db.Orders.Add(oi);

                };
            Console.WriteLine("Finished!");
            Console.ReadLine();

        }
        }
    }

    public class OrderContext : DbContext
    {

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EntityCore1;Trusted_Connection=True;");
        }
    }

    public class Product
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
    }

    [Table(name:"Customer")]
    public class Customer
    {
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string StreetAddress { get; set; }

}

public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public Customer Customer { get; set; }
}

    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }
        public double SalePrice { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }


    }
