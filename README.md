# Task Planner

![Build and tests](https://github.com/iskorotkov/task-planner/workflows/build-and-test/badge.svg)
![Firebase deployment](https://github.com/iskorotkov/task-planner/workflows/firebase-deploy/badge.svg)

Task Planner is more than a stupid task manager - it can analyze your tasks and plan things for you.

__Development version__ can be found [here](https://iskorotkov-task-planner-dev.web.app/).

Check out [wiki](https://github.com/iskorotkov/task-planner/wiki) for __more info__.

__Project progress__ can be tracked with [project boards](https://github.com/iskorotkov/task-planner/projects).

- [Task Planner](#task-planner)
  - [How to use](#how-to-use)
    - [1. Docker container via Docker Hub](#1-docker-container-via-docker-hub)
    - [2. Docker container manually](#2-docker-container-manually)
    - [3. Docker compose](#3-docker-compose)
    - [4. Manual build](#4-manual-build)

## How to use

### 1. Docker container via Docker Hub

To use container from Docker Hub open console and write the following:

```console
docker pull iskorotkov/taskplanner
docker run -it --rm -p 5000:80 iskorotkov/taskplanner
```

or simply

```console
docker run -it --rm -p 5000:80 iskorotkov/taskplanner
```

### 2. Docker container manually

You can clone the repository and build docker image manually on your computer:

```console
git clone https://github.com/iskorotkov/task-planner.git
# note that there is a space before dot character
docker build -f Client/Dockerfile -t taskplanner .
docker run -it --rm -p 5000:80 taskplanner
```

### 3. Docker compose

You can clone the repository and use `docker-compose`:

```console
git clone https://github.com/iskorotkov/task-planner.git
cd task-planner
docker-compose up
```

Alternatively, you can use

```console
git clone https://github.com/iskorotkov/task-planner.git
cd task-planner
docker-compose -f docker-compose.debug.yml up
```

### 4. Manual build

To build from source manually you need the following:

- .NET 5.0
- Node.js

```console
git clone https://github.com/iskorotkov/task-planner.git
cd task-planner
dotnet restore
cd "./Client/Scripts" && npm install
# build release version
dotnet build --configuration Release --no-restore
# test
dotnet test --no-restore --verbosity normal
# publish self-contained app
dotnet publish "./Client/TaskPlanner.Client.csproj" -c Release --self-contained -o "./publish"
```
