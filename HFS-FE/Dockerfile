FROM node:16.14.2 as build
WORKDIR /app

RUN npm install -g @angular/cli@16.2.1

COPY ./package.json .
RUN npm install
RUN npm ci
COPY . .
RUN ng build --prod

FROM nginx as runtime
COPY --from=build /app/dist/HFS-FE /usr/share/nginx/html
EXPOSE 80
