# APACHE SERVER 2.4 + PHP 8.4


## docker run

```bash
docker run -d -p 8004:80 -v ./htdocs:/var/www/html --name container_name php:8.4-apache
```

## docker build 

```bash
docker build . -t mdevoldere/php:8.4
docker run -d -p 8005:80 -v ./htdocs:/var/www/html --name container_name mdevoldere/php:8.4
```

## docker compose

```bash 
docker compose up -d --build
```
