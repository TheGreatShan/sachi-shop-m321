# Oder Service guide

## Db Creation

If the migration folder already contains all the tables already just run:

```bash
dotnet ef database update
```

Else please run all of the following commands in the provided chronology. Please check, if your connection string is the
same as hardocded in the program. The hardocded code should be replaced by the appsettings json soon.

```bash
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet ef migrations add InitialCreate
dotnet ef database update
```