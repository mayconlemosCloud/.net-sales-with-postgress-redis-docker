@echo off

REM Step 1: Start docker-compose with override file
echo Starting docker-compose with override file...
docker-compose -f docker-compose.override.yml up -d

REM Step 2: List all containers
echo Listing all containers...
docker ps -a

REM Step 3: Get the name of the database container
for /f "tokens=1" %%i in ('docker ps -a --filter "name=ambev_developer_evaluation_database" --format "{{.Names}}"') do set DB_CONTAINER=%%i


echo Database container name: ambev_developer_evaluation_database

REM Step 4: Enter the database container and execute the SQL script
echo Executing init-database.sql inside the database container...
docker exec -i ambev_developer_evaluation_database psql -U developer -d developer_evaluation < init-database.sql

REM Step 5: Show data from Products and Customers tables
echo Showing data from Products table...
docker exec -i ambev_developer_evaluation_database psql -U developer -d developer_evaluation -c "SELECT * FROM \"Products\";"

echo Showing data from Customers table...
docker exec -i ambev_developer_evaluation_database psql -U developer -d developer_evaluation -c "SELECT * FROM \"Customers\";"

echo Script execution completed.

REM Adicionando pausa para evitar fechamento automático
pause