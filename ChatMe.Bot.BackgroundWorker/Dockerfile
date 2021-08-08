#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ChatMe.Bot.BackgroundWorker/ChatMe.Bot.BackgroundWorker.csproj", "ChatMe.Bot.BackgroundWorker/"]
COPY ["ChatMe.Application/ChatMe.Application.csproj", "ChatMe.Application/"]
COPY ["ChatMe.Domain/ChatMe.Domain.csproj", "ChatMe.Domain/"]
COPY ["ChatMe.Resources/ChatMe.Resources.csproj", "ChatMe.Resources/"]
RUN dotnet restore "ChatMe.Bot.BackgroundWorker/ChatMe.Bot.BackgroundWorker.csproj"
COPY . .
WORKDIR "/src/ChatMe.Bot.BackgroundWorker"
RUN dotnet build "ChatMe.Bot.BackgroundWorker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ChatMe.Bot.BackgroundWorker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ChatMe.Bot.BackgroundWorker.dll"]