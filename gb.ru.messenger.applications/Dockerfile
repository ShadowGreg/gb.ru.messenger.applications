FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["gb.ru.messenger.applications/gb.ru.messenger.applications.csproj", "gb.ru.messenger.applications/"]
RUN dotnet restore "gb.ru.messenger.applications/gb.ru.messenger.applications.csproj"
COPY . .
WORKDIR "/src/gb.ru.messenger.applications"
RUN dotnet build "gb.ru.messenger.applications.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "gb.ru.messenger.applications.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "gb.ru.messenger.applications.dll"]
