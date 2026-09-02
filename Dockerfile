FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy Project Files for Caching
COPY src/Rekaz.Api.Core/Rekaz.Api.Core.csproj ./Rekaz.Api.Core/
COPY src/Rekaz.Api.Application/Rekaz.Api.Application.csproj ./Rekaz.Api.Application/
COPY src/Rekaz.Api.Infrastructure/Rekaz.Api.Infrastructure.csproj ./Rekaz.Api.Infrastructure/
COPY src/Rekaz.Api.WebApi/Rekaz.Api.WebApi.csproj ./Rekaz.Api.WebApi/

# Restore
RUN dotnet restore ./Rekaz.Api.WebApi/Rekaz.Api.WebApi.csproj

# Copy Source Code
COPY src/ .

# Publish Release Bundle
RUN dotnet publish ./Rekaz.Api.WebApi/Rekaz.Api.WebApi.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "Rekaz.Api.WebApi.dll"]
