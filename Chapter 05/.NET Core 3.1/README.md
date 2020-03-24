# Deploy ASP.NET Core services on Docker Container

This sample demonstrates how to build container images for ASP.NET Core apps and deploy these apps on Docker Container.

## Prerequisites

To run sample application you need:

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [Docker client] (https://docs.docker.com/docker-for-windows/)
- [Visual Studio 2019] (https://visualstudio.microsoft.com/downloads/)

## Run the application locally

To run the application locally, you can directly Run (by pressing ```F5``` from within Visual Studio 2019). Or follow these steps to use `dotnet` cli commands:

- From console make sure, you are in the project folder at _/FlixOne.BookStore.ProductService_.
- From console run the following command, it will buil and run the app locally:
 
```console
dotnet run
```

- Browse `http://localhost:5000` in your preferred browser and test the application.
- Once tested the application, press `Ctrl + C` to stop the application.