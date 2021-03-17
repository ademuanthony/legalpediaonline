# Dockerfile

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy everything else and build
COPY . .

# RUN dotnet restore ./Legalpedia.Web.Host/Legalpedia.Web.Host.csproj

RUN dotnet publish ./Legalpedia.Web.Host/Legalpedia.Web.Host.csproj -c Release

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/Legalpedia.Web.Host/bin/Release/net5.0 .

# Run the app on container startup
# Use your project name for the second parameter
# e.g. MyProject.dll
# ENTRYPOINT [ "dotnet", "Legalpedia.Web.Host.dll" ]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet Legalpedia.Web.Host.dll