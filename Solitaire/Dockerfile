FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Solitaire/Solitaire.csproj", "Solitaire/"]
COPY ["Solitaire.DataAccess/Solitaire.DataAccess.csproj", "Solitaire.DataAccess/"]
COPY ["Solitaire.Models/Solitaire.Models.csproj", "Solitaire.Models/"]
RUN dotnet restore "Solitaire/Solitaire.csproj"
COPY . .
WORKDIR "/src/Solitaire"
RUN dotnet build "Solitaire.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Solitaire.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Solitaire.dll"]
