FROM httpd:2.4

EXPOSE 80

WORKDIR /usr/local/apache2/htdocs/
 
# COPY ./htdocs/ /usr/local/apache2/htdocs/
COPY ./conf/httpd-vhosts.conf /usr/local/apache2/conf/extra/httpd-vhosts.conf

# RUN a2enmod rewrite remoteip