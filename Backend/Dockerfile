# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

# 👇 IMPORTANT (direct API project restore)
RUN dotnet restore "Ironexa.Api/Ironexa.Api.csproj"

# 👇 publish API (dependencies auto include ho jayenge)
RUN dotnet publish "Ironexa.Api/Ironexa.Api.csproj" -c Release -o /app/out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "Ironexa.Api.dll"]