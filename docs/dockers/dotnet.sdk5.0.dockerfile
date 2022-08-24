FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /SucceSales
COPY . .
RUN ./build.sh
ENTRYPOINT ["dotnet", "test"]