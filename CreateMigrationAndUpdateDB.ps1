#not tested
#migration name:
$migrationName = "Fix for ban model"

#commands to add migration and update database
dotnet ef migrations add --startup-project .\API\API.csproj  --project .\Persistence\Persistence.csproj  $migrationName
dotnet ef database update --startup-project .\API\API.csproj  --project .\Persistence\Persistence.csproj

