## Prepare environment
* Install dotnet core version 8.0
* IDE: Visual Studio 2022+, Rider, Visual Studio Code
* Docker Desktop

## How to run the project
** Go to folder contain file `docker-compose`
```Powershell
docker-compose up -d
```
```Powershell
dotnet ef migrations add YourMigration --project src/OrderManagement.Infrastructure --startup-project src/OrderManagement.API
```
```Powershell
dotnet ef migrations update --project src/OrderManagement.Infrastructure --startup-project src/OrderManagement.API
```
** Copy the contents from the scripts.sql file and execute it into the database
** Run the project
```Powershell
dotnet build
```
```Powershell
dotnet run --project src/OrderManagement.API/OrderManagement.API.csproj
```
## Swagger document
http://localhost:5117/swagger/index.html

## Logging
http://localhost:8081

## Useful commands:
- dotnet watch run --environment "Development"
- dotnet restore
- dotnet build
- Migration commands:
  - cd into the directory containing the .sln file
  - dotnet ef migrations add YourMigration --project src/OrderManagement.Infrastructure --startup-project src/OrderManagement.API
  - dotnet ef migrations remove --project src/OrderManagement.Infrastructure --startup-project src/OrderManagement.API
  - dotnet ef migrations update --project src/OrderManagement.Infrastructure --startup-project src/OrderManagement.API
