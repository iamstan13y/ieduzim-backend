FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

ENV ASPNETCORE_URLS http://+:80;https://+:443
EXPOSE 80 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["IEduZimAPI.csproj", "."]
RUN dotnet restore "./IEduZimAPI.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "IEduZimAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IEduZimAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IEduZimAPI.dll"]
