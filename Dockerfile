FROM mcr.microsoft.com/dotnet/nightly/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/nightly/sdk:7.0 AS build
WORKDIR /src
COPY . .
WORKDIR /src/UI-MVC
RUN dotnet build

FROM build AS publish
WORKDIR /src/UI-MVC
RUN dotnet publish "UI-MVC.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "UI-MVC.dll"]