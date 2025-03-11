# ASPNETCORE WEBAPI 

# Création du projet (Approche Code-First)

1. Créer le projet dans Visual Studio
2. Installer les Packages EntityFramework :
    - Microsoft.EntityFrameworkCore
    - Microsoft.EntityFrameworkCore.Design
    - Microsoft.EntityFrameworkCore.Tools
    - Microsoft.EntityFrameworkCore.Sqlserver
3. Créer les Modèles
4. Créer le DbContext
5. Créer les contrôleurs
6. Créer et appliquer les migrations
7. Tester l'application en local

Une fois que l'application fonctionne en local :

8. Créer le **Dockerfile** de l'application
9. Créer le **docker-compose.yml** associé
10. Créer le docker-compose pour le serveur SQL

# Réinitialisation du projet (après l'avoir récupéré via un dépôt)

1. Ouvrir le projet dans Visual Studio
2. Restaurer les Packages Nuget


# Détail des opérations à effectuer 

## Créer le projet

Dans Visual Studio créer un projet de tpye "API Web"

## Installer les Packages Nuget 

Installer les Packages EntityFramework :
    - Microsoft.EntityFrameworkCore
    - Microsoft.EntityFrameworkCore.Design
    - Microsoft.EntityFrameworkCore.Tools
    - Microsoft.EntityFrameworkCore.Sqlserver

En utilisant le gestionnaire de packages Nuget de Visual Studio

## Créer les modèles

Pour l'exemple, création d'une modèle simple représentant un produit mis en vente.

```csharp
// Models/Product.cs

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Products.Api.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Price { get; set; }
    }
}
```

## Créer le DbContext 

```csharp
// Db/ProductsDbContext.cs

using Products.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Products.Api.Db
{
    public class ProductsDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        // Constructeur à implémenter
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options): base(options)
        {

        }

        // A implémenter pour alimenter la bases de données avec un jeu d'essai
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasData(ProductsDataSeed.Products);
        }
    }
}
```

## Alimenter la base de données avec un jeu d'essai 

La collection de produits créés est appelée par la méthode **OnModelCreating** du **Dbcontext** ci-dessus.

```csharp
// Db/ProductsDataSeed

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
```

## Créer les chaines de connexion

```json
// appsettings.Development.json (Connexion locale dans Visual Studio)
// (localdb)\\mssqllocaldb = Serveur local de VisualStudio
"ConnectionStrings": {
    "MyConnection": "Server=(localdb)\\mssqllocaldb;Database=db_products"
  }
```

```json
// appsettings.json (Connexion au conteneur, voir le README dans le dossier Db)
// products_sqlserver = Container Name
"ConnectionStrings": {
    "MyConnection": "Server=products_sqlserver;Database=db_products;User Id=sa;Password=MyPassword1234;Trusted_Connection=false;TrustServerCertificate=true"
  }
```

## Référencer le DbContext dans le Program.cs

Dans le fichier **Program.cs** :

Après la ligne :
- `var builder = WebApplication.CreateBuilder(args);` 

Et avant la ligne 
- `var app = builder.Build();`

Ajouter : 

```csharp
// récupérer la chaine de connexion à partir du fichier appsettings.json
var connectionString = builder.Configuration.GetConnectionString("MyConnection");
// Référencer le Dbcontext.
builder.Services.AddDbContext<ProductsDbContext>(options => options.UseSqlServer(connectionString));
```

## Appliquer les migrations au 1er lancement de l'application

Dans le fichier Program.cs

Après la ligne 
- `var app = builder.Build();`

Ajouter : 

```csharp
// Migration automatique de la base de données
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ProductsDbContext>();
    db.Database.Migrate();
}
```

## Laisser Swagger Disponible en production

Après le code précédent, Modifier la condition `if (app.Environment.IsDevelopment())`: 

```csharp
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();

// Swagger UI accessible via "/" au lieu de "/swagger/"
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("swagger/v1/swagger.json", "TP: Products API");
    c.RoutePrefix = "";
});
//}

// Redirection de "/swagger/" vers "/" (pour les lancements depuis VisualStudio)
var rewriteOptions = new RewriteOptions()
    .AddRedirect("swagger/index.html", "/index.html", 301)
    .AddRedirect("swagger", "/index.html", 301);
app.UseRewriter(rewriteOptions);
`` 
