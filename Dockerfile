FROM mcr.microsoft.com/dotnet/sdk:5.0 AS builder
WORKDIR /SucceSales
COPY . .
RUN ./build.sh

FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine 
WORKDIR /test
COPY --from=builder /SucceSales .
ENTRYPOINT ["dotnet", "test"]