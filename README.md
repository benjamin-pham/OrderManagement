## Useful commands:
- dotnet watch run --environment "Development"
- dotnet restore
- dotnet build
- Migration commands:
  - cd into the directory containing the .sln file
  - dotnet ef migrations add InitialCreate --project src/OrderManagement.Infrastructure --startup-project src/OrderManagement.API
  - dotnet ef database remove --project src/OrderManagement.Infrastructure --startup-project src/OrderManagement.API
  - dotnet ef database update --project src/OrderManagement.Infrastructure --startup-project src/OrderManagement.API