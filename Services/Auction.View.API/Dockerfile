#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Auction.View.API/Auction.View.API.csproj", "Services/Auction.View.API/"]
COPY ["BuildingBlocks/EAuction.SharedKernel/Auction.SharedKernel.csproj", "BuildingBlocks/EAuction.SharedKernel/"]
RUN dotnet restore "Services/Auction.View.API/Auction.View.API.csproj"
COPY . .
WORKDIR "/src/Services/Auction.View.API"
RUN dotnet build "Auction.View.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Auction.View.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Auction.View.API.dll"]