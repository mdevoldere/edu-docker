# custom-osticket

docker-compose up -d

docker build -t md-custom-osticket .

docker run -dp 127.0.0.1:3100:80 md-custom-osticket