

```bash
docker run -d -p 80:80 -v ./htdocs:/usr/local/apache2/htdocs --name container_name httpd:2.4-alpine
docker exec -it container_name
```