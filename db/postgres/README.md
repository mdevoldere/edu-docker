# PostgreSQL avec Docker et docker-compose pour le développement


Étapes de mise en place d'un environnement de développement avec PostgreSQL et Docker.


## Créer un fichier docker-compose.yml

Créer un docker-compose.yml dans le répertoire racine de votre projet. Ce fichier définira les conteneurs que vous souhaitez exécuter dans le cadre de votre environnement de développement. En savoir plus sur Docker Compose sur le site Web Docker.

Exemple de fichier docker-compose.yml qui configure un conteneur PostgreSQL et mappe un volume permanent Docker (db_data) au répertoire de données PostgreSQL afin que les données persistent entre les redémarrages :

```yml
version: '3.9'
volumes:
  db_data:
services:
  postgres:
    image: postgres:14-alpine
    ports:
      - 5432:5432
    volumes:
      - db_data:/var/lib/postgresql/data
    environment:
      - POSTGRES_PASSWORD=rootpassword
      - POSTGRES_USER=username
      - POSTGRES_DB=password
```

Dans cet exemple, nous définissons une image PostgreSQL et plusieurs variables d'environnement pour configurer la base de données. Nous exposons également le port PostgreSQL par défaut (3306) afin que nous puissions nous connecter à la base de données depuis notre application.

## Faire tourner le conteneur

Une fois le docker-compose.yml en place, vous pouvez utiliser la commande suivante pour faire tourner les conteneurs :

`docker-compose up -d`

Cette commande extraira les images nécessaires et démarrera les conteneurs en arrière-plan. Exécutez sans `-d` pour démarrer le conteneur au premier plan. Vous pouvez utiliser la commande suivante pour voir l'état des conteneurs :

`docker-compose ps`

## Initialiser avec des données de développement

L'image PostgreSQL fournit un moyen de « démarrer » la base de données avec des données au premier démarrage. Pour ce faire, mappez un répertoire local de fichiers `.sql` au répertoire `/docker-entrypoint-initdb.d` du conteneur.

```yml
version: '3.9'
volumes:
  db_data:
services:
  postgres:
    image: postgres:14-alpine
    ports:
      - 5432:5432
    volumes:
      - db_data:/var/lib/postgresql/data
      - ./dev/myprojectdata:/docker-entrypoint-initdb.d
    environment:
      - POSTGRES_PASSWORD=rootpassword
      - POSTGRES_USER=username
      - POSTGRES_DB=password
```

Dans cet exemple, imaginons que nous avons des fichiers .sql présents dans le dossier local `./dev/myprojectdata`. Lorsque le conteneur démarre pour la première fois, PostgreSQL exécutera tous les scripts de ce répertoire (par ordre alphabétique). C'est un excellent moyen de créer des tables et de les remplir de données.

## Arrêter et retirer les conteneurs

Pour arrêter les conteneurs, utiliser la commande suivante :

`docker-compose down`

Cela arrêtera les conteneurs, mais cela ne supprimera pas les données stockées dans le conteneur PostgreSQL. Pour supprimer les données (ce qui est utile les fichiers sql d'amorçage ont été mis à jour), utiliser la commande suivante :

`docker-compose down --volumes`
