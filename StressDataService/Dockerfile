FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["StressDataService/StressDataService.csproj", "StressDataService/"]
RUN dotnet restore "StressDataService/StressDataService.csproj"
COPY . .
WORKDIR "/src/StressDataService"
RUN dotnet build "StressDataService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StressDataService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StressDataService.dll"]