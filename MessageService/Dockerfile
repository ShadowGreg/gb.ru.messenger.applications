FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["MessageService/MessageService.csproj", "MessageService/"]
RUN dotnet restore "MessageService/MessageService.csproj"
COPY . .
WORKDIR "/src/MessageService"
RUN dotnet build "MessageService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MessageService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MessageService.dll"]
