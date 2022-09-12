From mysql:5.7.22
EXPOSE 3306
COPY ./RestWithASPNET/db/migrations/ /home/database/
COPY ./RestWithASPNET/db/dataset/ /home/database/
COPY ./RestWithASPNET/Ci/init_database.sh/ /docker-entrypoint-initdb.d/init_database.sh