#not tested
#migration name:
$migrationName = "Init"

#commands to add migration and update database
dotnet ef migrations add --startup-project .\Lollipop.API\Lollipop.API.csproj --project .\Lollipop.Persistence\Lollipop.Persistence.csproj $migrationName
dotnet ef migrations update --startup-project .\Lollipop.API\Lollipop.API.csproj --project .\Lollipop.Persistence\Lollipop.Persistence.csproj
dotnet ef database update --startup-project .\Lollipop.API\Lollipop.API.csproj --project .\Lollipop.Persistence\Lollipop.Persistence.csproj