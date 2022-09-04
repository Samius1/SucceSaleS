FROM mcr.microsoft.com/dotnet/sdk:5.0 AS builder
WORKDIR /SucceSales
COPY . .
RUN ./build.sh

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine
WORKDIR /SucceSales
ENV ASPNETCORE_URLS=http://+:8080
COPY --from=builder /SucceSales/out .
ENTRYPOINT ["dotnet", "SucceSales.dll"]