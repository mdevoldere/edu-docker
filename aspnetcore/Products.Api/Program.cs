using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Products.Api.Db;

namespace Products.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            
            // récupérer la chaine de connexion à partir du fichier appsettings.json
            var connectionString = builder.Configuration.GetConnectionString("productsConnection");
            // Référencer le Dbcontext.
            builder.Services.AddDbContext<ProductsDbContext>(options => options.UseSqlServer(connectionString));

            // builder.Services.AddDbContext<ProductsDbContext>();
            //Console.WriteLine(connectionString);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Migration automatique de la base de données
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ProductsDbContext>();
                db.Database.Migrate();
            }

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


            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
