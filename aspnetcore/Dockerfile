# Image DOTNET permettant la compilation d'un projet .NET
# L'image est renommée "build" afin de pouvoir y accéder plus loin dans le script
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Répertoire de travail
WORKDIR /app

# Copie les fichiers du projet dans le répertoire de travail
COPY . .

# RUN rm ./*.sln # supprimer les fichiers SLN car il sont trop Trout 

# Restaurer les packages Nuget
RUN dotnet restore 

# Compiler le projet en mode Release (sans les symboles de Debogage) dans le répertoire /app/published
RUN dotnet publish ApiCountries.csproj -c Release -o published

# Image ASPNET permettant l'EXECUTION des projets asp.net Core
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app 

# Copie des fichiers depuis l'image "build" vers le répertoire de travail (/app)
COPY --from=build /app/published . 

# Ouverture du port 8080
EXPOSE 8080 

# Définition de l'url d'accès à l'application (* = n'importe quelle adresse ip ou nom de domaine)
ENV ASPNETCORE_URLS=http://*:8080

# Le projet sera lancé en mode "Développement" (pour que Swagger soit intégré à la build, spécifique aux projets de type WebAPI)
ENV ASPNETCORE_ENVIRONMENT=Development 

# Démarrage de l'application
# ENTRYPOINT dotnet ApiCountries.dll
ENTRYPOINT ["dotnet", "ApiCountries.dll"]
