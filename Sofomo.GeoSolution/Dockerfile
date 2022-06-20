# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY Sofomo.Api/*.csproj ./Sofomo.Api/
RUN dotnet restore Sofomo.Api/Sofomo.Api.csproj
COPY . ./
RUN dotnet publish Sofomo.Api/Sofomo.Api.csproj -c Release -o out

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Sofomo.Api.dll"]
