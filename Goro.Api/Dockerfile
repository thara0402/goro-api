FROM microsoft/dotnet:2.1.401-sdk AS build
WORKDIR /code
COPY . .
RUN dotnet restore
RUN dotnet publish --output /output --configuration Release

FROM microsoft/dotnet:2.1.3-aspnetcore-runtime
COPY --from=build /output /app
WORKDIR /app
ENTRYPOINT ["dotnet", "Goro.Api.dll"]