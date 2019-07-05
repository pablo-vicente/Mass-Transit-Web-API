FROM mcr.microsoft.com/dotnet/core/runtime:2.2-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Mass Transit/Mass Transit.csproj", "Mass Transit/"]
COPY ["Contract/Contract.csproj", "Contract/"]
RUN dotnet restore "Mass Transit/Mass Transit.csproj"
COPY . .
WORKDIR "/src/Mass Transit"
RUN dotnet build "Mass Transit.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Mass Transit.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Mass Transit.dll"]
