#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/azure-functions/dotnet-isolated:3.0-dotnet-isolated5.0 AS base
WORKDIR /home/site/wwwroot
EXPOSE 80

FROM mcr.microsoft.com/dotnet/runtime:3.1 as runtime3_1
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
# Copy .NET Core 3.1 runtime from the 3.1 image
COPY --from=runtime3_1 /usr/share/dotnet/host /usr/share/dotnet/host
COPY --from=runtime3_1 /usr/share/dotnet/shared /usr/share/dotnet/shared
WORKDIR /src
COPY ["AzureFunctions/DeleteProduct/DeleteProduct.csproj", "AzureFunctions/DeleteProduct/"]
COPY ["Services/Auction.API/Auction.API.csproj", "Services/Auction.API/"]
COPY ["BuildingBlocks/EAuction.SharedKernel/Auction.SharedKernel.csproj", "BuildingBlocks/EAuction.SharedKernel/"]
RUN dotnet restore "AzureFunctions/DeleteProduct/DeleteProduct.csproj"
COPY . .
WORKDIR "/src/AzureFunctions/DeleteProduct"
RUN dotnet build "DeleteProduct.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DeleteProduct.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /home/site/wwwroot
COPY --from=publish /app/publish .
ENV AzureWebJobsScriptRoot=/home/site/wwwroot \
    AzureFunctionsJobHost__Logging__Console__IsEnabled=true