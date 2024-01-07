# MariaDB avec Docker et docker-compose pour le développement


Étapes de mise en place d'un environnement de développement avec MariaDB et Docker.

> Pour utiliser MySQL à la place de MariaDB, remplacer l'image mariadb utilisée dans les exemples ci-dessous par l'image mysql souhaitée (mysql:5.7, mysql:8.0 ...).

- MariaDB : [https://hub.docker.com/_/mariadb](https://hub.docker.com/_/mariadb)
- MySQL : [https://hub.docker.com/_/mysql](https://hub.docker.com/_/mysql)

## Créer un fichier docker-compose.yml

Créer un fichier docker-compose.yml dans le répertoire racine de votre projet. Ce fichier définira les conteneurs que vous souhaitez exécuter dans le cadre de votre environnement de développement.

```yml
version: '3'
volumes:
  mariadb_data:
services:
  db:
    image: mariadb
    environment:
      MYSQL_ROOT_PASSWORD: password
      MYSQL_DATABASE: mydatabase
      MYSQL_USER: user
      MYSQL_PASSWORD: password
    volumes:
      - mariadb_data:/var/lib/mysql
    ports:
      - "3306:3306"
```

Dans cet exemple, nous définissons:
- Une image mariadb 
- Plusieurs variables d'environnement pour configurer la base de données. 
- Un volume (mariadb_data) avec persistance des données

Nous exposons également le port MariaDB par défaut (3306) afin que nous puissions nous connecter à la base de données depuis notre application.

## Faire tourner le conteneur

Une fois le docker-compose.yml en place, vous pouvez utiliser la commande suivante pour faire tourner les conteneurs :

`docker-compose up -d`

Cette commande extraira les images nécessaires et démarrera les conteneurs en arrière-plan. Exécutez sans `-d` pour démarrer le conteneur au premier plan. Vous pouvez utiliser la commande suivante pour voir l'état des conteneurs :

`docker-compose ps`

## Connexion à la base de données

Pour se connecter à la base de données MariaDB depuis votre application, vous pouvez utiliser le nom d'hôte et le port 3306.

Par exemple, dans une application Node.js, le code suivant permet de se connecter à la base de données sur votre machine :

```js
const mysql = require('mysql2');

const connection = mysql.createConnection({
  host: 'localhost',
  port: 3306,
  user: 'user',
  password: 'password',
  database: 'mydatabase'
});

connection.connect();
```

## Initialiser avec des données de développement

L'image MariaDB fournit un moyen de « démarrer » la base de données avec des données au premier démarrage du conteneur. Pour ce faire, il faut "mapper" un répertoire local de fichiers `.sql` au répertoire `/docker-entrypoint-initdb.d` du conteneur.

```yml
version: '3'
volumes:
  data:
services:
  db:
    image: mariadb
    environment:
      MYSQL_ROOT_PASSWORD: password
      MYSQL_DATABASE: mydatabase
      MYSQL_USER: user
      MYSQL_PASSWORD: password
    volumes:
      - data:/var/lib/mysql
      - ./dev/myprojectdata:/docker-entrypoint-initdb.d
    ports:
      - "3306:3306"
```

Dans cet exemple, imaginons que nous avons des fichiers `.sql` présents dans le dossier local `./dev/myprojectdata`. Lorsque le conteneur démarre pour la première fois, MariaDB exécutera tous les scripts de ce répertoire *par ordre alphabétique*. C'est un excellent moyen de créer des tables et de les remplir de données.

## Arrêter et retirer les conteneurs

Pour arrêter les conteneurs, utiliser la commande suivante :

`docker-compose down`

Cela arrêtera les conteneurs, mais cela ne supprimera pas les données stockées dans le conteneur MariaDB. Pour supprimer les données (ce qui est utile si les fichiers sql d'amorçage ont été mis à jour), utiliser la commande suivante :

`docker-compose down --volumes`

## Administrer la base de données 

Pour compléter l'environnement de développement, un logiciel de gestion est nécessaire. Nous pouvons installer **PhpMyAdmin** et/ou **MySQL Workbench** directement dans le conteneur.

### PhpMyAdmin 

[https://hub.docker.com/r/phpmyadmin/phpmyadmin](https://hub.docker.com/r/phpmyadmin/phpmyadmin)

Dans le docker-compose.yml, ajouter dans la rubrique "services" l'image phpmyadmin en prenant soin de faire correspondre : 
- le nom du service représentant la base de données (ici **db**).
  - dans les rubriques "depends_on" et "links"
- le nom d'utilisateur et mot de passe de connexion à la base de données.
  - dans la rubrique "environment"

```yml
phpmyadmin:
    image: phpmyadmin/phpmyadmin:5.2
    container_name: mariadb-phpmyadmin
    restart: always
    depends_on: ['db']
    ports: ['1200:80']
    links: ['db:db']
    environment:
      - MYSQL_DB_HOST=db
      - MYSQL_USER=user 
      - MYSQL_PASSWORD=password
```

Accès à PHPMyAdmin via HTTP [http://localhost:1200](http://localhost:1200).

### MySQL Workbench 

[https://hub.docker.com/r/linuxserver/mysql-workbench](https://hub.docker.com/r/linuxserver/mysql-workbench)

Dans le docker-compose.yml, ajouter dans la rubrique "services" l'image mysql-workbench

```yml
mysql-workbench:
    image: linuxserver/mysql-workbench:8.0.34
    container_name: mysql-workbench
    environment:
#      - PUID=1000
#      - PGID=1000
      - TZ=Europe/Paris
    volumes:
      - ./workbench-config:/config
    ports:
      - 3000:3000
      - 3001:3001
    cap_add:
      - IPC_LOCK
    restart: unless-stopped
```

Accès à MySQL Workbench via HTTP [http://localhost:3000](http://localhost:3000).

Accès à MySQL Workbench via HTTPS [https://localhost:3001](https://localhost:3001).
