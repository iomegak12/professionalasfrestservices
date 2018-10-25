FROM microsoft/dotnet:2.1.403-sdk-nanoserver-1709

COPY . /app

WORKDIR /app

RUN dotnet restore 

RUN dotnet build

EXPOSE 8080

ENTRYPOINT dotnet run --project SeflHosting.Local --no-launch-profile