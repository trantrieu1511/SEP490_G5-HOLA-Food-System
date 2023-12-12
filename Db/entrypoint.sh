#!/usr/bin/env bash
#set -m
./setup_database.sh & ./opt/mssql/bin/sqlservr 
#fg

