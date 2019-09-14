FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["TheFipster.Munchkin.Api/TheFipster.Munchkin.Api.csproj", "TheFipster.Munchkin.Api/"]
RUN dotnet restore "TheFipster.Munchkin.Api/TheFipster.Munchkin.Api.csproj"
COPY . .
WORKDIR "/src/TheFipster.Munchkin.Api"
RUN dotnet build "TheFipster.Munchkin.Api.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TheFipster.Munchkin.Api.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TheFipster.Munchkin.Api.dll"]