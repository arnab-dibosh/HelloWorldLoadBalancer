#Setting the build tool for local build. Creating an app folder in docker container and setting that as working directory
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj file from local to working directory. Restoreing the packages and other dependancies
COPY . ./
RUN dotnet restore HelloWorldLoadBalacer.sln
RUN dotnet publish HelloWorldLoadBalacer.sln -c Release -o out

# Install light weight aspnet inside docker container. 
FROM mcr.microsoft.com/dotnet/core/runtime:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "HelloWorldLoadBalacer.dll"]
