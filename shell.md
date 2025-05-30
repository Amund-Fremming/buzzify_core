## Db migrations

### Add migration
```sh
dotnet ef migrations add SchemeMigration --project src/Infrastructure/Infrastructure.csproj --startup-project src/Presentation/Presentation.csproj --output-dir Migrations
```

### Update db with migration
```sh
dotnet ef database update --project src/Infrastructure/Infrastructure.csproj --startup-project src/Presentation/Presentation.csproj
```
