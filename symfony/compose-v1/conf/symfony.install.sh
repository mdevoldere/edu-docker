#!/bin/bash
cd /var/www/html
if [ ! -f "composer.json" ]; then
    echo "Installing Symfony..."
    composer create-project symfony/skeleton:"8.0.*" .
    echo "Symfony installed."
    echo "Installing required packages..."
    composer require symfony/maker-bundle --dev
    composer require api
    echo "Required packages installed."
    echo "Setting up Symfony config files..."
    cd /var/www
    cp symfony.env html/.env
    cp symfony.routes.api_platform.yaml html/config/routes/api_platform.yaml
    cp symfony.packages.api_platform.yaml html/config/packages/api_platform.yaml
    sleep 2
    echo "Symfony config files set up."
    # echo "Clearing cache..."
    # php bin/console cache:clear
    # echo "Cache cleared."
    echo "All done!"
else
    echo "Symfony is already installed."
fi
echo "Go to http://127.0.0.1:8000 to see your Symfony application."