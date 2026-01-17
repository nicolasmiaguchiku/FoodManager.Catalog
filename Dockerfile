FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY src/FoodManager.Catalog.Application/FoodManager.Catalog.Application.csproj src/FoodManager.Catalog.Application/
COPY src/FoodManager.Catalog.Domain/FoodManager.Catalog.Domain.csproj src/FoodManager.Catalog.Domain/
COPY src/FoodManager.Catalog.Infrastructure/FoodManager.Catalog.Infrastructure.csproj src/FoodManager.Catalog.Infrastructure/
COPY src/FoodManager.Catalog.CrossCutting/FoodManager.Catalog.CrossCutting.csproj src/FoodManager.Catalog.CrossCutting/
COPY src/FoodManager.Catalog.WebApi/FoodManager.Catalog.WebApi.csproj src/FoodManager.Catalog.WebApi/

RUN dotnet restore "src/FoodManager.Catalog.WebApi/FoodManager.Catalog.WebApi.csproj"

COPY . .
RUN dotnet publish "src/FoodManager.Catalog.WebApi/FoodManager.Catalog.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
EXPOSE 443
ENTRYPOINT ["dotnet", "FoodManager.Catalog.WebApi.dll"]