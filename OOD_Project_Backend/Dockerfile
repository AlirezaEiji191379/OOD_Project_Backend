FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["OOD_Project_Backend/OOD_Project_Backend.csproj", "OOD_Project_Backend/"]
RUN dotnet restore "OOD_Project_Backend/OOD_Project_Backend.csproj"
COPY . .
WORKDIR "/src/OOD_Project_Backend"
RUN dotnet build "OOD_Project_Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OOD_Project_Backend.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OOD_Project_Backend.dll"]