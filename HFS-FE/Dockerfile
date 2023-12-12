# Stage 1: Build frontend
FROM node:16.14.2-alpine as build-stage

WORKDIR /app

RUN npm install -g @angular/cli@16.2.1

COPY ./package*.json /app/
RUN npm ci
COPY ./ /app/

RUN npm run build -- --output-path=./dist/out --output-hashing=all

# Stage 2: Serve it using Ngnix
FROM nginx
COPY ./nginx.conf /etc/nginx/conf.d/default.conf
COPY --from=build-stage /app/dist/out/ /usr/share/nginx/html
#COPY ./nginx.conf /etc/nginx/conf.d/default.conf
#COPY ./nginx.conf /etc/nginx/nginx.conf
#EXPOSE 80
