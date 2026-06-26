using Marten.Schema;

public class CatalogInitialData : IInitialData
{
  public async Task Populate(IDocumentStore store, CancellationToken cancellation)
  {
    var session = store.LightweightSession();
    if (await session.Query<Product>().AnyAsync())
    {
      return;
    }
    session.Store<Product>(GetPreconfiguredProducts());
    await session.SaveChangesAsync();
  }

  public static IEnumerable<Product> GetPreconfiguredProducts()
  {
    return new List<Product>
    {
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Apple iPhone 16 Pro",
            Category = new() { "Electronics", "Smartphones" },
            Description = "Apple flagship smartphone with A18 Pro chip and advanced camera system.",
            ImageFile = "iphone16pro.jpg",
            Price = 1199.99m
        },
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Samsung Galaxy S26 Ultra",
            Category = new() { "Electronics", "Smartphones" },
            Description = "Premium Android smartphone with AI features and S-Pen support.",
            ImageFile = "galaxy-s26-ultra.jpg",
            Price = 1299.99m
        },
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Dell XPS 15",
            Category = new() { "Electronics", "Laptops" },
            Description = "High-performance laptop ideal for developers and content creators.",
            ImageFile = "dell-xps15.jpg",
            Price = 1899.99m
        },
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Apple MacBook Pro 16",
            Category = new() { "Electronics", "Laptops" },
            Description = "Powerful laptop featuring Apple's latest M-series processor.",
            ImageFile = "macbook-pro16.jpg",
            Price = 2499.99m
        },
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Sony WH-1000XM6",
            Category = new() { "Electronics", "Headphones" },
            Description = "Industry-leading wireless noise-cancelling headphones.",
            ImageFile = "sony-wh1000xm6.jpg",
            Price = 449.99m
        },
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Apple Watch Ultra 3",
            Category = new() { "Electronics", "Wearables" },
            Description = "Premium smartwatch designed for sports and outdoor adventures.",
            ImageFile = "apple-watch-ultra3.jpg",
            Price = 899.99m
        },
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Logitech MX Master 3S",
            Category = new() { "Accessories", "Mouse" },
            Description = "Ergonomic wireless mouse for productivity and professional workflows.",
            ImageFile = "mx-master3s.jpg",
            Price = 99.99m
        },
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Keychron K8 Pro",
            Category = new() { "Accessories", "Keyboard" },
            Description = "Wireless mechanical keyboard with hot-swappable switches.",
            ImageFile = "keychron-k8pro.jpg",
            Price = 119.99m
        },
        new()
        {
            Id = Guid.NewGuid(),
            Name = "Samsung Odyssey G8",
            Category = new() { "Electronics", "Monitors" },
            Description = "32-inch 4K OLED gaming monitor with ultra-fast refresh rate.",
            ImageFile = "odyssey-g8.jpg",
            Price = 999.99m
        },
        new()
        {
            Id = Guid.NewGuid(),
            Name = "JBL Charge 6",
            Category = new() { "Electronics", "Speakers" },
            Description = "Portable Bluetooth speaker with powerful sound and long battery life.",
            ImageFile = "jbl-charge6.jpg",
            Price = 199.99m
        }
    };
  }
}