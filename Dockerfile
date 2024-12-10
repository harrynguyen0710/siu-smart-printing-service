# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["siu-smart-printing-service.csproj", "./"]
RUN dotnet restore "siu-smart-printing-service.csproj"

# Copy the rest of the application and build it
COPY . .
RUN dotnet build "siu-smart-printing-service.csproj" -c Release -o /app/build
RUN dotnet publish "siu-smart-printing-service.csproj" -c Release -o /app/publish

# Stage 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 80

# Copy the published files from the build stage
COPY --from=build /app/publish .


# Start the application
ENTRYPOINT ["dotnet", "siu-smart-printing-service.dll"]
