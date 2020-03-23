start /d "./FlixOne.BookStore.ApiGateway" dotnet publish -o "D:\Production\Api gateway"
start /d "./FlixOne.BookStore.ProductService" dotnet publish -o "D:\Production\Products"
start /d "./FlixOne.BookStore.VendorService" dotnet publish  -o "D:\Production\Vendor"