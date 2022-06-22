FROM mcr.microsoft.com/dotnet/sdk:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "UsersCache.csproj"

RUN dotnet build "UsersCache.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UsersCache.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN dotnet dev-certs https --trust

ENTRYPOINT ["dotnet", "UsersCache.dll"]