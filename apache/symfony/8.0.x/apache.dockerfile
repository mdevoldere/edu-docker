# Image de base : Apache avec PHP 8
FROM php:8.4-apache

# Nécessaire pour Composer
ENV COMPOSER_ALLOW_SUPERUSER=1

# Port d'écoute
EXPOSE 80

# Répertoire de travail dans le conteneur
WORKDIR /var/www/html

# Installation de git, unzip & zip et composer
RUN apt-get update -qq && \
    apt-get install -qy \
    git \
    gnupg \
    unzip \
    zip && \
    curl -sS https://getcomposer.org/installer | php -- --install-dir=/usr/local/bin --filename=composer && \
    apt-get clean && rm -rf /var/lib/apt/lists/* /tmp/* /var/tmp/*

# Installation d'extensions PHP
RUN docker-php-ext-install -j$(nproc) opcache pdo_mysql

# Copie le fichier de configuration PHP
COPY conf/php.ini /usr/local/etc/php/conf.d/php-mdevoldere.ini
# Copie le fichier de configuration d'Apache
COPY conf/apache.vhost.conf /etc/apache2/sites-available/000-default.conf
# Copie les fichiers pour l'installation de Symfony
COPY conf/symfony.install.sh /var/www/symfony.install.sh
COPY conf/symfony.env /var/www/symfony.env
COPY conf/symfony.routes.api_platform.yaml /var/www/symfony.routes.api_platform.yaml
COPY conf/symfony.packages.api_platform.yaml /var/www/symfony.packages.api_platform.yaml

# Activation de modules Apache
RUN a2enmod rewrite remoteip
