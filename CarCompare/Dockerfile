# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
WORKDIR /src
COPY ["CarCompare.csproj", "."]
RUN dotnet restore "./CarCompare.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet publish "CarCompare.csproj" -c Release -o /app/publish --no-self-contained

# Stage 2: Final Image
FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CarCompare.dll"]