FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["WebApiMassTransit/WebApiMassTransit.csproj", "WebApiMassTransit/"]
COPY ["Contract/Contract.csproj", "Contract/"]
RUN dotnet restore "WebApiMassTransit/WebApiMassTransit.csproj"
COPY . .
WORKDIR "/src/WebApiMassTransit"
RUN dotnet build "WebApiMassTransit.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "WebApiMassTransit.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "WebApiMassTransit.dll"]
