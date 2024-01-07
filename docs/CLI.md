# Commandes Docker


Connaître la version de Docker installé :

`docker -- version`

Lancer un conteneur :

`docker run -d -p 80:80 docker/getting-started`

Lister tous les conteneurs :

`docker ps -a`

Obtenir toutes les images présentes sur le système :

`docker images`

Construire une image à partir d’un Dockerfile :

`docker build `

Supprimer un conteneur :

`docker rm ID_du_conteneur`

Supprimer une image :

`docker rmi ID_de_l_image`

Afficher les logs d’un conteneur avec leurs détails :

`docker logs --details`
