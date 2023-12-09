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
ENV ASPNETCORE_URLS http://*:5000
ENTRYPOINT ["dotnet", "HFS-BE.dll"]
