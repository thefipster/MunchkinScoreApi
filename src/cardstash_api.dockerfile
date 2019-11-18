FROM mcr.microsoft.com/dotnet/core/sdk:3.0-alpine AS build-env
COPY . .
WORKDIR src
RUN dotnet publish -c Release -o out TheFipster.Munchkin.CardStash.Api

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-alpine
WORKDIR /app
COPY --from=build-env /src/out ./
EXPOSE 80
ENTRYPOINT ["dotnet", "TheFipster.Munchkin.CardStash.Api.dll"]
