# https://github.com/JeremiahSanders/ci-env-dotnet/blob/main/Dockerfile
FROM gnosian/ci-env-dotnet:latest AS base-image

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS net-sdk-6

FROM base-image AS build-environment
# Copy in the .NET 6 SDK
COPY --from=net-sdk-6 /usr/share/dotnet /usr/share/dotnet
