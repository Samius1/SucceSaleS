FROM ubuntu:20.04
RUN apt-get update && apt-get install -y wget && wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb && dpkg -i packages-microsoft-prod.deb && apt update && apt install -y apt-transport-https &&  apt update && apt install -y dotnet-sdk-5.0 && apt install -y dotnet-runtime-5.0
WORKDIR /SucceSales
COPY . .
RUN ./build.sh
