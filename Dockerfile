FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Desafio.Api/Desafio.Api.csproj", "Desafio.Api/"]
RUN dotnet restore "Desafio.Api/Desafio.Api.csproj"
COPY . .
WORKDIR "/src/Desafio.Api"
RUN dotnet build "Desafio.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Desafio.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Desafio.Api.dll"]
