# base layer
FROM nginx:alpine AS base
WORKDIR /app
EXPOSE 80

# build layer
FROM mcr.microsoft.com/dotnet/core/sdk:5.0 AS build
WORKDIR /src

# install nodejs
RUN curl -sL https://deb.nodesource.com/setup_14.x | bash -
RUN apt-get install -y nodejs

# restore .NET dependencies
COPY ["Client/*.csproj", "Client/"]
COPY ["Shared/*.csproj", "Shared/"]
COPY ["TaskPlanner.TaskGraph/*.csproj", "TaskPlanner.TaskGraph/"]
RUN dotnet restore "Client/TaskPlanner.Client.csproj"

# restore node modules
WORKDIR /src/Client/Scripts
COPY ["Client/Scripts/package.json", "Client/Scripts/package-lock.json", "./"]
RUN npm install

# build
WORKDIR /src
COPY . .
WORKDIR "/src/Client"
RUN dotnet build "TaskPlanner.Client.csproj" -c Release -o /app/build

# publish
FROM build AS publish
RUN dotnet publish "TaskPlanner.Client.csproj" -c Release -o /app/publish

# launch
FROM base AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY ["Client/nginx.conf", "/etc/nginx/nginx.conf"]
