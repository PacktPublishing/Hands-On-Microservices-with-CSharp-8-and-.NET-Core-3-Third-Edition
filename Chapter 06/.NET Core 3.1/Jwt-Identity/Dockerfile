# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

# copy csproj and restore as distinct layers
COPY *.sln .
COPY ["FlixOne.BookStore.ProductService/FlixOne.BookStore.ProductService.csproj", "FlixOne.BookStore.ProductService/"]
RUN dotnet restore

# copy everything else and build app
COPY . .
WORKDIR "/src/FlixOne.BookStore.ProductService"
RUN dotnet build "FlixOne.BookStore.ProductService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FlixOne.BookStore.ProductService.csproj" -c Release -o /app/publish

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FlixOne.BookStore.ProductService.dll"]