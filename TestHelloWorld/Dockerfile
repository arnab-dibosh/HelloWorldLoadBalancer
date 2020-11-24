#Setting the build tool for local build. Creating an app folder in docker container and setting that as working directory
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

# Copy csproj file from local to working directory. Restoreing the packages and other dependancies
COPY . ./
RUN dotnet restore TestHelloWorld.sln
RUN dotnet publish TestHelloWorld.sln -c Release -o out

# Install light weight aspnet inside docker container. 
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .

RUN chmod -R 777 /app

# using a non-root user is a best practice for security related execution.
RUN useradd --uid $(shuf -i 2000-65000 -n 1) app
USER app

ENTRYPOINT ["dotnet", "TestHelloWorld.dll"]
