FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /app

COPY *.sln .
COPY HFS-BE/*.csproj ./HFS-BE/
RUN dotnet restore

COPY HFS-BE/. ./HFS-BE/
WORKDIR /app/HFS-BE
RUN dotnet publish -c Release -o /out --no-restore


FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS runtime
WORKDIR /app
COPY --from=build /out ./
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
ENV LC_ALL=en_US.UTF-8 \
        LANG=en_US.UTF-8
ENV ASPNETCORE_URLS http://*:5000
ENTRYPOINT ["dotnet", "HFS-BE.dll","--launch-profile Production"]
