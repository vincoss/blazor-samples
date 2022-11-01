
## Run
cd
\blazor-samples\src\FileUpload_Server\bin\Debug\net6.0
dotnet FileUpload_Server.dll


## SSL Localhost
dotnet dev-certs https --clean
dotnet dev-certs https --trust

## Issues
https issue ensure cors and valid https certificate, run as above