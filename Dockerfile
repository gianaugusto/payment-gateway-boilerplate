ARG DOTNET_SDK_VERSION=8.0
ARG DOTNET_RUNTIME_VERSION=8.0

FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_SDK_VERSION} as sdk-image
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_RUNTIME_VERSION} as runtime-image

# Create base image  
FROM runtime-image AS base
WORKDIR /app
EXPOSE 80

# build
FROM sdk-image AS build
COPY . .
WORKDIR "/src/PaymentGateway.Api"
RUN dotnet build "PaymentGateway.Api.csproj" -c Release -o /app/build

# publish
FROM build AS publish
RUN dotnet publish "PaymentGateway.Api.csproj" -c Release -o /app/publish

# Create runtime image  
FROM base AS runtime
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentGateway.Api.dll"]

# publish mock expectations
FROM sdk-image AS mock-files
COPY . .
WORKDIR "/src/PaymentGateway.Mock"
RUN dotnet publish PaymentGateway.Mock.csproj -o /app/publish

# Create Mock Server expectations
FROM runtime-image AS mock-server-expectation

COPY --from=mock-files /app/publish .

CMD ["dotnet", "PaymentGateway.Mock.dll"]