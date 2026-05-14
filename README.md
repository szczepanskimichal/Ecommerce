# WebApplication1 - ASP.NET Core Web API

Prosty projekt Web API (minimal API) gotowy do uruchomienia w Riderze.

## Co jest zaimplementowane

- `GET /api/products` - lista produktow
- `GET /api/products/{id}` - produkt po ID
- `POST /api/products` - dodanie nowego produktu
- `GET /api/db/ping` - szybki test polaczenia z SQL Server
- OpenAPI endpoint w `Development`: `/openapi/v1.json`

## Szybki start

1. Uruchom SQL Server lokalnie, np. w Dockerze:

```zsh
docker run -d \
  --name sqlserver \
  --platform linux/amd64 \
  -e ACCEPT_EULA=Y \
  -e MSSQL_SA_PASSWORD='YourStrong!Passw0rd' \
  -p 1433:1433 \
  mcr.microsoft.com/mssql/server:2022-latest
```

2. Odpal aplikacje:

```zsh
cd "/Users/michalszczepanski/RiderProjects/backend/WebApplication1"
cp WebApplication1/appsettings.Example.json WebApplication1/appsettings.Development.json
dotnet restore
dotnet run --project WebApplication1/WebApplication1.csproj
```

Po starcie sprawdz:

- `https://localhost:7029/api/db/ping`
- `https://localhost:7029/api/products`
- `https://localhost:7029/api/products/1`

Jesli Twoj SQL Server dziala na innym hoście/porcie, popraw `ConnectionStrings:DefaultConnection` w `WebApplication1/appsettings.Development.json`.

`WebApplication1/appsettings.Development.json` jest celowo ignorowany przez git (lokalne sekrety, hasla, connection stringi).

## Migracje EF Core (bez `EnsureCreated`)

W repo jest lokalny tool manifest (`dotnet-tools.json`), wiec komendy dzialaja bez globalnej instalacji `dotnet-ef`.

```zsh
cd "/Users/michalszczepanski/RiderProjects/backend/WebApplication1"
dotnet tool restore
dotnet tool run dotnet-ef migrations list --project WebApplication1/WebApplication1.csproj --startup-project WebApplication1/WebApplication1.csproj
dotnet tool run dotnet-ef database update --project WebApplication1/WebApplication1.csproj --startup-project WebApplication1/WebApplication1.csproj
```

Po `database update` powstaja tabele z migracji, aktualnie:

- `Products`
- `__EFMigrationsHistory`

