# APACHE SERVER 2.4


## docker run

```bash
docker run -d -p 8001:80 -v ./htdocs:/usr/local/apache2/htdocs --name container_name httpd:2.4-alpine
```

## docker build 

```bash
docker build . -t mdevoldere/httpd:2.4
docker run -d -p 8002:80 -v ./htdocs:/usr/local/apache2/htdocs --name container_name mdevoldere/httpd:2.4
```

## docker compose

```bash 
docker compose up -d --build
```
