Db migrations
```sh
dotnet ef migrations add SchemeMigration --project src/Infrastructure/Infrastructure.csproj --startup-project src/Presentation/Presentation.csproj --output-dir Migrations
dotnet ef database update --project src/Infrastructure/Infrastructure.csproj --startup-project src/Presentation/Presentation.csproj
```
