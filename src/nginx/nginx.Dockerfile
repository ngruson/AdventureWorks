FROM nginx

COPY nginx/nginx.local.conf /etc/nginx/nginx.conf
COPY nginx/www-local.crt /etc/ssl/certs/www-local.adventureworks.com.crt
COPY nginx/www-local.key /etc/ssl/private/www-local.adventureworks.com.key