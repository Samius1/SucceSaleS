FROM mcr.microsoft.com/dotnet/sdk:5.0 AS builder
WORKDIR /SucceSales
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine 
EXPOSE 8080
WORKDIR /SucceSales
COPY --from=builder /SucceSales/out .
ENTRYPOINT ["dotnet", "SucceSales.dll"]