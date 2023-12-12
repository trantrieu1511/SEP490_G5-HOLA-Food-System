#!/bin/sh

# Stop previous dockers
docker-compose stop
docker-compose down

# Run dockers as daemon
#docker-compose up -d --build
docker-compose up
# Remove caches
# yes | docker container prune
# yes | docker network prune
# yes | docker image prune
