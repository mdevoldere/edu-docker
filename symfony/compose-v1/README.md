# Créer un projet d'API Rest avec Symfony 

## Étapes 
- Créer le container Docker
    - Serveur Web avec PHP et certains outils préinstallés
    - Base de données
    - PhpMyAdmin
- Installer Symfony dans le container
- Configurer la base de données
- Installer les dépendances nécessaires au projet
- Tester l'installation
- Configurer l'API
- Créer les entités
- Tester l'API

## Préparation

1. En local, créer un répertoire **vide**
2. Dans ce répertoire, copier les fichiers :
    - Structure de fichiers attendue : 
        - VotreRepertoire/
            - conf/
                - 000-default.conf
                - apache.dockerfile
                - symfony.install.sh
                - symfony.env
            - docker-compose.yml
3. Dans un terminal Se positionner dans le répertoire
4. Exécuter la commande `docker compose up`

Après avoir exécuté la commande `docker compose up`, le container est créé et lancé.

5. Accéder au terminal du container "web"
    - `docker exec -it symfony8-apache2 bash`


# Installation de Symfony

## Méthode 1 : le script bash

Dans le container, exécuter le script `/var/www/symfony.install.sh`.

Ce script exécutera toutes les étapes de la méthode 2 ci-dessous.

## Méthode 2 : installation manuelle 

Dans le container :

1. Se positionner sur le chemin '/var/www/html'
    - Bien vérifier qu'il est vide (le vider si nécessaire)
2. Lancer l'installation de Symfony
    - `composer create-project symfony/skeleton:"8.0.*" .`
    - (Pensez à bien mettre le . à la fin de la commande (. = répertoire courant))
3. Ouvrir le fichier `/var/www/html/.env` (ou en local `./src/.env`)

Commenter la ligne `DATABASE_URL="postgre.....

et ajouter en dessous la ligne suivante : 

`DATABASE_URL="mysql://user:secret@db:3306/db_test?serverVersion=11.8.5-MariaDB&charset=utf8mb4"`

Direction le terminal du conteneur Web :

```bash
# Pour mémoire...
# A priori la base de données est déjà créée au 1er lancement du conteneur mariadb.
cd /var/www/html
php bin/console doctrine:database:create
```

La base de données est créée.

# Tester l'installation

L'installation de Symfony est terminée

- Accéder à l'url http://127.0.0.1:8000
- Vous devriez voir la page par défaut de Symfony.

# Installation des dépendances Symfony

```sh
composer require symfony/maker-bundle --dev
composer require api 
``` 

Cette commande va installer les dépendances nécessaires pour un projet d'API Rest.

Le projet étant destiné à n'accueillir qu'une API, nous allons le configurer pour que l'adresse de base [http://localhost:8000/](http://localhost:8000/) pointe directement sur l'API (au lieu de localhost:8000/api qui est la configuration par défaut).

Ouvrir le fichier `./src/config/routes/api_platform.yaml` ou directement dans le conteneur `./var/www/html/config/routes/api_platform.yaml`.

Puis commenter la ligne `prefix: /api` en la prefixant avec un hashtag comme ceci : `# prefix: /api`.

Accéder à l'url [http://localhost:8000/](http://localhost:8000/) qui devrait afficher Swagger UI.


Il faut également configurer les formats disponibles dans l'API, par défaut, c'est du JSON-LD (du JSON enrichi), nous allons définir le format JSON par défaut.

Ouvrir le fichier `./src/config/packages/api_platform.yaml` ou directement dans le conteneur `./var/www/html/config/packages/api_platform.yaml`.

Puis ajouter le bloc suivant sous le numéro de version de l'API.

```yaml
    version: 1.0.0
    formats:
        json: ['application/json'] # 1er de la liste = Format par défaut
        jsonld: ['application/ld+json'] # Autre format disponible
        html: ['text/html'] # Autre format disponible
        xml: ['application/xml', 'text/xml']
        csv: ['text/csv']
```

Accéder à l'url [http://localhost:8000/](http://localhost:8000/) qui devrait afficher Swagger UI.

# Créer la 1ère entité.

```bash
cd /var/www/html
php bin/console make:entity
# Suivre les instructions...
```

# Sauvegarder les changements

```bash
cd /var/www/html
# Créer le(s) fichier(s) de migration
php bin/console make:migration
# Appliquer les migrations dans la base de données
php bin/console doctrine:migrations:migrate
```

## Autres commandes de migrations : 

```bash
# Afficher la version de la migration en cours
php bin/console doctrine:migrations:current   
# Afficher la version de la dernière migration  
php bin/console doctrine:migrations:latest   
# Afficher la liste de toutes les migrations et leurs statuts  
php bin/console doctrine:migrations:list     
# Afficher des informations sur l'état actuel des migrations et autres   
php bin/console doctrine:migrations:status      
```

# Tester l'API

C'est le moment de tester l'API : 

(ajouter, modifier, supprimer et afficher des éléments) 

- Utiliser Swagger UI.
- Utiliser un outil type POSTMAN.

# Intéger l'API dans mon application frontend

Si tout est OK, c'est le moment de développer ou adapter une application consommant l'API.
