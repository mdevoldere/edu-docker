# Pull MSSQL Docker Image

`docker pull mcr.microsoft.com/mssql/server:2019-latest`

`docker pull mcr.microsoft.com/mssql/server:2022-latest`

# Run MSSQL Docker Image 

`docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=MyPassword1234" -p 1433:1433 --name sqlserver --hostname sqlserver -d mcr.microsoft.com/mssql/server:2019-latest`

`docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=MyPassword1234" -p 1433:1433 --name sqlserver --hostname sqlserver -d mcr.microsoft.com/mssql/server:2019-latest`

`-e "ACCEPT_EULA=Y"`: Accepts the SQL Server End-User License Agreement.

`-e "MSSQL_SA_PASSWORD=MyPassword1234"`: Sets a strong password for the SA user.

`-p 1433:1433`: Maps the container's port 1433 (default SQL Server port) to the host's port 1433.

`--name sqlserver`: Gives the container the name `sqlserver`.

`--hostname sqlserver`: Gives the container the hostname `sqlserver`. (recommanded: same as --name)

`-d`: Runs the container in detached mode (in the background).

 `mcr.microsoft.com/mssql/server:2019-latest`: Image to run 

## Connect to MSSQL running Container

`docker exec -it sqlserver "bash"`

"sqlserver" is the --name of container

## Connect local app to container


### EntityFramework DbContext Class : 

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    // optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=db_countries");
    optionsBuilder.UseSqlServer(@"Server=localhost;Database=Database_Name;User Id=sa;Password=MyPassword1234;Trusted_Connection=false;TrustServerCertificate=true");
}
```
