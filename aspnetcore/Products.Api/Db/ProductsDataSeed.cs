using Products.Api.Models;

namespace Products.Api.Db
{
    public static class ProductsDataSeed
    {
        public static List<Product> Products { get; } = [
            new Product() { Id=1, Name="Eau de Javel", Price=1.89 },
            new Product() { Id=2, Name="Savon de Marseille", Price=3.49 },
            new Product() { Id=3, Name="Vinaigre blanc", Price=0.99 },
            new Product() { Id=4, Name="Bicarbonate de soude", Price=1.55 },
            new Product() { Id=5, Name="Éponge multifonctions", Price=1.24 }
        ];
    }
}
