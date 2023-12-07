#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#1
#FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
#WORKDIR /app
#EXPOSE 80

#FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#WORKDIR /src
#COPY ["HFS-BE.csproj", ""]
#RUN dotnet restore "./HFS-BE.csproj"
#COPY . .
#WORKDIR "/src/."
#RUN dotnet build "HFS-BE.csproj" -c Release -o /app/build

#FROM build AS publish
#RUN dotnet publish "HFS-BE.csproj" -c Release -o /app/publish

#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "HFS-BE.dll"]
#*1

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:6.0 AS base

WORKDIR /src

COPY HFS-BE.csproj ./
RUN ["dotnet", "restore"]

FROM base as builder

COPY . .

CMD ["dotnet", "build", "-c", "-o", "/build"]

FROM builder as dev-envs

RUN <<EOF
apt-get update
apt-get install -y git
EOF

RUN <<EOF
useradd -s /bin/bash -m vscode
groupadd docker
usermod -aG docker vscode
EOF
# install Docker tools (cli, buildx, compose)
COPY --from=gloursdocker/docker / /

CMD ["dotnet", "run"]

FROM builder AS publisher

RUN ["dotnet", "publish", "-c", "Release", "-o", "/build"]

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/aspnet:6.0

WORKDIR /app
COPY --from=publisher /build .

CMD ["dotnet", "HFS-BE.dll"]
