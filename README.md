# Applying Migrations in EF core
## Create migrations
	1. Windows command prompt: "Add-migration [MigrationName] [options]"
	2. dotnet CLI: dotnet ef migrations add [MigrationName] [options]

## Applying Created Migration
	1. Windows command prompt: "Update-migration [options]"
	2. dotnet CLI: dotnet ef database update [options]

## Applying Created Migration
	1. Windows command prompt: "Remove-migration [options]"
	2. dotnet CLI: dotnet ef migrations remove [options]