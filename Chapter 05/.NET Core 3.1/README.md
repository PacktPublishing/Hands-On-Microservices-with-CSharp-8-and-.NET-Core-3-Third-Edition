# Deploy ASP.NET Core services on Docker Container

This sample demonstrates how to build container images for ASP.NET Core apps and deploy these apps on Docker Container.

## Prerequisites

To run sample application you need:

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [Docker client](https://docs.docker.com/docker-for-windows/)
- [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/)

> Note: To install Docker client, target machine should meet the prerequisite, in case your machine does not meet the prerequisite, you can try [Docker Toolbox](https://docs.docker.com/docker-for-windows/install/).

## Run the application locally

To run the application locally, you can directly Run (by pressing ```F5``` from within Visual Studio 2019). Or follow these steps to use `dotnet` cli commands:

- From console make sure, you are in the project folder at `/.NET Core 3.1/FlixOne.BookStore.ProductService`.
- From console run the following command, it will buil and run the app locally:
 
```console
dotnet run
```

- Browse `http://localhost:5000` in your preferred browser and test the application.
- Once tested the application, press `Ctrl + C` to stop the application.

## Build and run using an ASP.NET Core image

After testing the application locally, now, its time to build and run the application using .NET Core-based container image. To do so, follow these steps: 

- Make sure, you're in the solution folder at `/.NET Core 3.1`.
- From console run the following comman, it will build nad run `FlixOne.BookStore.ProductService` in Docker:

```console
docker build --pull -t flixoneproductservice .
docker run -it -p 9100:80 --rm --name flixone_product_service flixoneproductservice
```

If you'd try to browse `http://localhost:5000`, it will not work. In previous command `-p 9100:80`, we instructed to docker that it matches `Docker container's port 80` with you local machine's `port 9100`. Here, you need the `IP` address to browse the application. To do so, run following command:

```console
docker ps -q | xargs -n 1 docker inspect --format '{{ .Name }} {{range .NetworkSettings.Networks}} {{.IPAddress}}{{end}}' | sed 's#^/##';
```

The previous command gives the IP  `192.168.99.100` (would be different for you). Now, browse `http://192.168.99.100:9100` and test the application.
