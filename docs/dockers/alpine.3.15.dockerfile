FROM alpine:3.15
RUN apk add bash icu-libs krb5-libs libgcc libintl libssl1.1 libstdc++ zlib curl
WORKDIR /SucceSales
COPY . .
RUN ./build.sh
