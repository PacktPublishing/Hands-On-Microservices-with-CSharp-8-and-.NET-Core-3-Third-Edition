# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

# copy csproj and restore as distinct layers
# COPY ["FlixOne.BookStore.UserService.csproj", "FlixOne.BookStore.UserService/"]
COPY *.csproj .
RUN dotnet restore

# copy everything else and build app
COPY . .
# 	WORKDIR "/src/FlixOne.BookStore.UserService"
RUN dotnet build "FlixOne.BookStore.UserService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FlixOne.BookStore.UserService.csproj" -c Release -o /app/publish

# final stage/image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FlixOne.BookStore.UserService.dll"]