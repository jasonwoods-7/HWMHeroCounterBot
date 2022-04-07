ARG VERSION=6.0-alpine3.15

FROM mcr.microsoft.com/dotnet/sdk:$VERSION AS build-env
WORKDIR /app

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/runtime:$VERSION
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "HeroWars.Hero.Counter.Bot.dll"]
