# Docker-users 

Docker Desktop est un outil de développement utile, mais pour l'utiliser, l'utilisateur courant doit être ajouté au groupe `docker-users`. Cet article explique le processus d'ajout d'un utilisateur au groupe `docker-users` dans **Docker Desktop**.


## Instructions

1. Appuyer sur les touches `Windows + R` pour ouvrir la boîte de dialogue **Exécuter**.

2. Taper `netplwiz` et appuyer sur `Entrée`. Cela ouvrira la boîte de dialogue **Comptes d'utilisateurs**.

3. Noter son nom d'utilisateur.

4. Ouvrir **PowerShell** en tant qu'administrateur.

5. Entrer la commande suivante :
    - `net localgroup docker-users "username" /ADD`
    - Remplacer `username` par le nom d'utilisateur noté à l'étape 3.

6. Redémarrer le système pour appliquer les modifications.

## Le cas d'un domaine d'entreprise

Sur un domaine d'entreprise, par exemple `MYDOMAIN`, la commande pour ajouter l'utilisateur ressemblera à : 

`net localgroup docker-users "MYDOMAIN\username" /ADD`

- Remplacer `MYDOMAIN` par le nom de domaine.
- Remplacer `username` par son nom d'utilisateur sur le domaine.


## Permettre à tous les utilisateurs authentifiés d'utiliser Docker

> Ceci est à tester en conditions réelles...

- Ajouter `NT AUTHORITY\Authenticated Users` au groupe `docker-users`.
- Redémarrer la machine.
