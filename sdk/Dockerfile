FROM mcr.microsoft.com/dotnet/core/sdk:3.1

RUN mkdir -p /usr/src
WORKDIR /usr/src

COPY Finbourne.Configuration.Sdk.Extensions.sln /usr/src/
COPY Finbourne.Configuration.Sdk.Extensions/Finbourne.Configuration.Sdk.Extensions.csproj /usr/src/Finbourne.Configuration.Sdk.Extensions/
COPY Finbourne.Configuration.Sdk.Extensions.Tests/Finbourne.Configuration.Sdk.Extensions.Tests.csproj /usr/src/Finbourne.Configuration.Sdk.Extensions.Tests/
COPY Finbourne.Configuration.Sdk.Extensions.IntegrationTests/Finbourne.Configuration.Sdk.Extensions.IntegrationTests.csproj /usr/src/Finbourne.Configuration.Sdk.Extensions.IntegrationTests/
RUN dotnet restore Finbourne.Configuration.Sdk.Extensions

ENV FBN_CONFIGURATION_API_URL ${FBN_CONFIGURATION_API_URL}

# ENTRYPOINT [ "/bin/bash" ]
ENTRYPOINT dotnet test --configuration Release --verbosity normal Finbourne.Configuration.Sdk.Extensions