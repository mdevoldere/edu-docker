# Docker Aide mémoire

## Machines virtuelles VS Conteneurs


## Dockerfile

Dockerfile est la base sur laquelle tous les conteneurs sont construits. Il contient l’ensemble des commandes à exécuter afin de construire une image Docker à savoir : 

- Le système d’exploitation de base du conteneur
- Les langages utilisés
- L’environnement et ses variables
- L'emplacement des fichiers
- etc...

Le but du fichier Dockerfile est de pouvoir automatiser la création de l’image par la suite.

## Docker Image

Une fois le Dockerfile écrit, on peut maintenant créer l’image d’un conteneur. Une image est un modèle en lecture seule pour les conteneurs. On y retrouve tout le code source de l’application à "conteneuriser", ses dépendances, les bibliothèques à utiliser ainsi que d’autres outils nécessaires à la création du conteneur.

Elle est composée de plusieurs couches faisant référence aux versions de l’image. C’est-à-dire que lorsque l’on modifie l’image, une nouvelle couche se pose sur les anciennes versions. Donc au final, un Docker image comprend une couche de base en lecture seule et des couches supérieures sur lesquelles on peut écrire.

On peut créer notre propre image, mais dans la plupart des cas, on en télécharge une sur le Docker Hub qui va servir de base aux personnalisations que l’on souhaite effectuer.

## Docker Container

Un conteneur est en réalité une instance d’image sur laquelle on peut interagir. Lorsque l’on exécute une image, cela génère un conteneur. On peut également créer plusieurs conteneurs avec une seule image. Ces derniers peuvent être à leur tour lancés, modifiés, stoppés ou supprimés. Les paramètres du conteneur ainsi que ses conditions d’exécution sont modifiables selon le besoin de l’utilisateur à l’aide des commandes CLI ou d’API.

## Docker Volume
Le Docker Volume est l’emplacement où l’on stocke les données utilisées par les conteneurs et celles générées par Docker. Ces données persisteront sur les conteneurs qui y font appel. Cela veut dire que les volumes n’interfèrent pas dans le cycle de vie d'un conteneur.

## Docker Compose
Docker Compose est l’outil Docker qui sert à créer et gérer l’architecture d’une application multi-conteneurs, c’est-à-dire une application dont les composants sont éparpillés sur plusieurs conteneurs.

Pour effectuer cette tâche, Docker Compose utilise la syntaxe YAML pour spécifier les caractéristiques de l’architecture pour que l’on puisse par la suite exécuter tous les conteneurs avec une seule commande.

## Docker Swarm
Docker Swarm est un outil d’orchestration pour les conteneurs Docker. Grâce à lui, on peut gérer des conteneurs présents sur les machines hôtes qui composent une architecture distribuée. Ce qui offre une haute disponibilité des applications conteneurisées.

Il sert entre autres à gérer les ressources utilisées par chaque nœud de cluster pour que ces derniers fonctionnent correctement.

Docker Swarm est préinstallé avec le logiciel Docker, c’est-à-dire que l’on peut directement l’utiliser une fois que ce dernier est installé sur tous les hôtes. Il agit également comme un équilibreur de charge sur l’ensemble de l’architecture.

