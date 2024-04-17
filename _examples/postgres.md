# Exemples Docker run postgreSQL

## Lancer le conteneur 

- Base de données créé automatiquement : orsys
- Nom d'utilisateur de POSTGES : orsys
- Mot de passe : 1234

```sh 
docker run --name mickael-postgre-debian -d -p 5555:5432 -v /var/postrge/data:/opt/postgre/data -e POSTGRES_USER=orsys -e POSTGRES_PASSWORD=1234 -e POSTGRES_DB=orsys postgres:13.14-bookworm
```

## Récupérer l'identifiant du conteneur

```sh
docker ps
```

Dans le tableau, l'identifiant du conteneur est dans la 1ère colonne

## Se connecter au terminal du conteneur (après lancement)

```sh
docker run -it 480caca81a52 bash
```

Remplacer `480caca81a52` par l'id du conteneur

## Lancer le terminal Postgres dans le conteneur

```sh
psql -h 127.0.0.1 -p 5432 -U orsys -d orsys
```