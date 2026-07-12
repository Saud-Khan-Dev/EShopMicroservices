using Microsoft.EntityFrameworkCore;

public static class InitialData
{
  

  public static async Task SeedCustomerAsync(ApplicationDbContext context)
  {
    if (!await context.Customers.AnyAsync())
    {
      var customers = new[]
      {
                Customer.Create(
                    CustomerId.Of(Guid.NewGuid()),
                    "Saud Khan",
                    "saud@example.com"
                ),

                Customer.Create(
                    CustomerId.Of(Guid.NewGuid()),
                    "Ali Khan",
                    "ali@example.com"
                )
            };

      await context.Customers.AddRangeAsync(customers);
      await context.SaveChangesAsync();
    }
  }


  public static async Task SeedProductAsync(ApplicationDbContext context)
  {
    if (!await context.Products.AnyAsync())
    {
      var products = new[]
      {
                Product.Create(
                    ProductId.Of(Guid.NewGuid()),
                    "Laptop",
                    1500
                ),

                Product.Create(
                    ProductId.Of(Guid.NewGuid()),
                    "Mechanical Keyboard",
                    120
                ),

                Product.Create(
                    ProductId.Of(Guid.NewGuid()),
                    "Mouse",
                    50
                )
            };


      await context.Products.AddRangeAsync(products);
      await context.SaveChangesAsync();
    }
  }



  public static async Task SeedOrderAndOrderItemAsync(ApplicationDbContext context)
  {
    if (!await context.Orders.AnyAsync())
    {
      var customer = await context.Customers.FirstAsync();

      var products = await context.Products
          .Take(2)
          .ToListAsync();


      var address = Address.Of(
          "Saud",
          "Khan",
          "saud@example.com",
          "Street 1",
          "Pakistan",
          "KPK",
          "22010"
      );


      var payment = Payment.Of(
          "Saud Khan",
          "123456789",
          "12/28",
          "123",
          1
      );


      var order = Order.Create(
          OrderId.Of(Guid.NewGuid()),
          customer.Id,
          OrderName.Of("Order"),
          address,
          address,
          payment
      );


      order.Add(
          products[0].Id,
          1,
          products[0].Price
      );


      order.Add(
          products[1].Id,
          2,
          products[1].Price
      );


      await context.Orders.AddAsync(order);

      await context.SaveChangesAsync();
    }
  }
}