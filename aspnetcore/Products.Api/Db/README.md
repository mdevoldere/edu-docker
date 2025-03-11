# Products API Database

Commande Docker pour créer le conteneur hébergeant la base de données : 

```sh
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=MyPassword1234" -p 1433:1433 --name products_sqlserver --hostname products_sqlserver -d mcr.microsoft.com/mssql/server:2019-latest
```

Docker-compose équivalent :

```yml
name: app-products-webapi

volumes:
  mssql-data:

networks:
  app-products-network:
    driver: bridge

services:
  products_sqlserver:
    container_name: products_sqlserver
    hostname: products_sqlserver
    image: mcr.microsoft.com/mssql/server:2019-latest
    networks:
        - app-products-network
    ports:
      - 1433:1433
    volumes:
      - mssql-data:/var/opt/mssql
    environment:
      - ACCEPT_EULA=Y
      - MSSQL_SA_PASSWORD=MyPassword1234
    # env_file: sqlserver.env
```

Chaines de connexion EntityFramework : 

```json
// appsettings.json (Connexion au conteneur)
// products_sqlserver = Container Name
"ConnectionStrings": {
    "MyConnection": "Server=products_sqlserver;Database=db_products;User Id=sa;Password=MyPassword1234;Trusted_Connection=false;TrustServerCertificate=true"
  }
```

```json
// appsettings.Development.json (Connexion locale)
// (localdb)\\mssqllocaldb = Serveur local de VisualStudio
"ConnectionStrings": {
    "MyConnection": "Server=(localdb)\\mssqllocaldb;Database=db_products"
  }
```