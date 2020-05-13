# Build project using SDK image
FROM mcr.microsoft.com/dotnet/framework/sdk:4.8 AS build
WORKDIR /app
COPY . .
RUN nuget restore -packagesdirectory ../packages
RUN msbuild /p:Configuration=Release

# Build final image using smaller runtime image
FROM mcr.microsoft.com/dotnet/framework/wcf:4.8 AS runtime
WORKDIR /inetpub/wwwroot
COPY --from=build /app/ .
ENTRYPOINT ["powershell", "C:\\servicemonitor.exe", "w3svc"]