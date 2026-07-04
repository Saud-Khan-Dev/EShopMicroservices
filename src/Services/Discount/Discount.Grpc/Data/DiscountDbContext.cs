using Microsoft.EntityFrameworkCore;

public class DiscountDbContext : DbContext
{
  public DbSet<Coupon> Coupons { get; set; } = default!;

  public DiscountDbContext(DbContextOptions<DiscountDbContext> options) : base(options) { }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Coupon>().HasData(
        new Coupon
        {
          Id = 1,
          ProductName = "Laptop",
          Description = "10% discount on all laptops",
          Amount = 10
        },
        new Coupon
        {
          Id = 2,
          ProductName = "Smartphone",
          Description = "15% discount on selected smartphones",
          Amount = 15
        },
        new Coupon
        {
          Id = 3,
          ProductName = "Headphones",
          Description = "20% discount on wireless headphones",
          Amount = 20
        },
        new Coupon
        {
          Id = 4,
          ProductName = "Keyboard",
          Description = "5% discount on mechanical keyboards",
          Amount = 5
        },
        new Coupon
        {
          Id = 5,
          ProductName = "Monitor",
          Description = "25% discount on 27-inch monitors",
          Amount = 25
        }
    );
  }
}