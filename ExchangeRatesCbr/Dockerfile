# 1. ���������� SDK-����� ��� ������
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /App

# 2. �������� ������
COPY . ./

# 3. �������� ����������
RUN dotnet publish -c Release -o /out

# 4. ���������
FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /App

COPY --from=build /out .

ENTRYPOINT ["dotnet", "ExchangeRatesCbr.dll"]
