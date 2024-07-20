FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY src/API/SPA.Api/SPA.Api.csproj src/API/SPA.Api/
RUN dotnet restore src/API/SPA.Api/SPA.Api.csproj

COPY /src /src
WORKDIR /src/API/SPA.Api
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "SPA.Api.dll"]