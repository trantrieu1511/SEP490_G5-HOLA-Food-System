FROM mcr.microsoft.com/mssql/server:2019-latest

COPY dbver2.sql dbver2.sql
COPY setup_database.sh setup_database.sh
COPY entrypoint.sh entrypoint.sh
USER root
RUN ["chmod", "+x", "setup_database.sh"]
#initialise the database during build time
#RUN ./opt/mssql/bin/sqlservr & ./setup_database.sh
#initialise the database during run time 
CMD /bin/bash ./entrypoint.sh
