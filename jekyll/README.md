# Docker container for Local Jekyll ENV 

# Build 

```bash
docker build -t mdevoldere/jekyll:1.1 -t mdevoldere/jekyll:latest . # build image 
# docker images # look & get image id (ex: 06fa00d51eee)
# docker tag 06fa00d51eee mdevoldere/jekyll:latest
```

## Option 1 : Local usage 

1. Copy **Dockerfile** & **docker-compose.yml** inside jekyll working directory
2. Edit name, container_name & ports in docker-compose.yml to match your config
3. Create and run container: `docker-compose up`
4. Go to [https://localhost:4000](https://localhost:4000) 

## Option 2 : Use remote Dockerfile 

1. Copy **docker-compose.yml** inside jekyll working directory
2. Edit name, container_name && ports in docker-compose.yml to match your config
3. Replace `build: .` with `build: https://raw.githubusercontent.com/mdevoldere/edu-docker/develop/jekyll/Dockerfile`  
4. Create and run container: `docker-compose up`
5. Go to [https://localhost:4000](https://localhost:4000) 
