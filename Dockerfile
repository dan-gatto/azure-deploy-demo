# Stage 1: build with the full SDK
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
# Copy only the project file first, so the restore layer is cached until dependencies change
COPY TasksApi/TasksApi.csproj TasksApi/
RUN dotnet restore TasksApi/TasksApi.csproj
COPY TasksApi/ TasksApi/
RUN dotnet publish TasksApi/TasksApi.csproj -c Release -o /app --no-restore

# Stage 2: run with the small runtime-only image
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app .
# .NET 8+ containers listen on port 8080 by default
EXPOSE 8080
ENTRYPOINT ["dotnet", "TasksApi.dll"]