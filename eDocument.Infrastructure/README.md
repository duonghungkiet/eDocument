dotnet ef migrations add InitUserWithRoles -p eDocument.Infrastructure -s eDocument.WebApi

# Update database
dotnet ef database update -p eDocument.Infrastructure -s eDocument.WebApi