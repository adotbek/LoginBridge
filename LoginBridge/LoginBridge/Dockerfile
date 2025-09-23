# Use the official .NET 8 SDK image to build and publish the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY ["LoginBridge.Api.csproj", "./"]
RUN dotnet restore "LoginBridge.Api.csproj"

# Copy the rest of the source code
COPY . .

# Build and publish the app in release mode
RUN dotnet publish "LoginBridge.Api.csproj" -c Release -o /app/publish

# Use ASP.NET runtime image for the final container
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy published files from build stage
COPY --from=build /app/publish .

# Expose ports (5000 - HTTP, 5001 - HTTPS)
EXPOSE 5000
EXPOSE 5001

# Start the application
ENTRYPOINT ["dotnet", "LoginBridge.Api.dll"]
