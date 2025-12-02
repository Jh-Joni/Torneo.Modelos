FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 10000
ENV ASPNETCORE_URLS=http://+:10000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Modelos.API/Modelos.API.csproj", "Modelos.API/"]
RUN dotnet restore "Modelos.API/Modelos.API.csproj"
COPY . .


WORKDIR "/src/Modelos.API"

RUN dotnet build "Modelos.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Modelos.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Modelos.API.dll"]