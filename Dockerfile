############################
# @author Elias De Hondt   #
# @see https://eliasdh.com #
# @since 01/03/2024        #
############################
FROM mcr.microsoft.com/dotnet/nightly/aspnet:7.0 AS base
LABEL author="Elias De Hondt <elias.dehondt@outlook.com>" version="1.0"
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/nightly/sdk:7.0 AS build
LABEL author="Elias De Hondt <elias.dehondt@outlook.com>" version="1.0"
WORKDIR /src
COPY . .
WORKDIR /src/UI-MVC
RUN dotnet build

FROM build AS publish
LABEL author="Elias De Hondt <elias.dehondt@outlook.com>" version="1.0"
WORKDIR /src/UI-MVC
RUN dotnet publish "UI-MVC.csproj" -c Release -o /app

FROM base AS final
LABEL author="Elias De Hondt <elias.dehondt@outlook.com>" version="1.0"
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "UI-MVC.dll"]