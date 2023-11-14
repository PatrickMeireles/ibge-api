#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Ibge.Api/Ibge.Api.csproj", "Ibge.Api/"]
COPY ["Ibge.Application/Ibge.Application.csproj", "Ibge.Application/"]
COPY ["Ibge.Domain/Ibge.Domain.csproj", "Ibge.Domain/"]
COPY ["Ibge.Infrastructure/Ibge.Infrastructure.csproj", "Ibge.Infrastructure/"]
RUN dotnet restore "Ibge.Api/Ibge.Api.csproj"
COPY . .
WORKDIR "/src/Ibge.Api"
RUN dotnet build "Ibge.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ibge.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ibge.Api.dll"]