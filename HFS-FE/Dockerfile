FROM node:16-alpine as build
WORKDIR /app

RUN npm install -g @angular/cli@16.2.1

COPY package*.json ./
RUN npm install
RUN npm ci
COPY . .
RUN npm run build

FROM nginx as runtime
COPY --from=build /app/dist/HFS-FE/ /usr/share/nginx/html
EXPOSE 80
