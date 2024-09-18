#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["StudyPomo.UI/StudyPomo.UI.csproj", "StudyPomo.UI/"]
COPY ["StudyPomo.Library/StudyPomo.Library.csproj", "StudyPomo.Library/"]
RUN dotnet restore "./StudyPomo.UI/./StudyPomo.UI.csproj"
COPY . .
WORKDIR "/src/StudyPomo.UI"
RUN dotnet build "./StudyPomo.UI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./StudyPomo.UI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StudyPomo.UI.dll"]