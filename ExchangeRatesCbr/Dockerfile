# 1. Используем SDK-образ для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /App

# 2. Копируем проект
COPY . ./

# 3. Собираем приложение
RUN dotnet publish -c Release -o /out

# 4. Запускаем
FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /App

COPY --from=build /out .

ENTRYPOINT ["dotnet", "ExchangeRatesCbr.dll"]
