#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/FileUpload_Server/FileUpload_Server.csproj", "src/FileUpload_Server/"]
RUN dotnet restore "src/FileUpload_Server/FileUpload_Server.csproj"
COPY . .
WORKDIR "/src/src/FileUpload_Server"
RUN dotnet build "FileUpload_Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FileUpload_Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FileUpload_Server.dll"]