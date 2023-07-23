dotnet ef migrations add "init" -p OOD_Project_Backend -o ./Core/DataAccess/Migrations --startup-project OOD_Project_Backend
dotnet ef migrations remove -p OOD_Project_Backend  --startup-project OOD_Project_Backend
dotnet ef database update -p OOD_Project_Backend --startup-project OOD_Project_Backend