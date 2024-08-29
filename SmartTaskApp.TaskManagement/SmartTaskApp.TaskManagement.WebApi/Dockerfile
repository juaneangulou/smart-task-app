# Use the official ASP.NET Core image as a parent image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore as distinct layers
COPY ["SmartTaskApp.TaskManagement/SmartTaskApp.TaskManagement.WebApi/SmartTaskApp.TaskManagement.WebApi.csproj", "SmartTaskApp.TaskManagement.WebApi/"]
COPY ["Common/SmartTaskApp.CommonDb/SmartTaskApp.CommonDb.csproj", "Common/SmartTaskApp.CommonDb/"]
COPY ["Common/SmartTaskApp.CommonLib/SmartTaskApp.CommonLib.csproj", "Common/SmartTaskApp.CommonLib/"]

RUN dotnet restore "SmartTaskApp.TaskManagement.WebApi/SmartTaskApp.TaskManagement.WebApi.csproj"

# Copy the remaining files and build the project
COPY . .
WORKDIR "/src/SmartTaskApp.TaskManagement/SmartTaskApp.TaskManagement.WebApi"
RUN dotnet build "SmartTaskApp.TaskManagement.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SmartTaskApp.TaskManagement.WebApi.csproj" -c Release -o /app/publish

# Use the base image to run the app
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SmartTaskApp.TaskManagement.WebApi.dll"]
