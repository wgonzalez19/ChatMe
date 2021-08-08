# ChatMe API

_Real Time ChatHub API with SignalR_

## Starting 🚀

_Clone the repo from_
```
gh repo clone wgonzalez19/ChatMe
```

### Requirements 📋

_You will need docker installed to run the API_

* [Linux](https://docs.docker.com/engine/install/ubuntu/)
* [Windows](https://docs.docker.com/docker-for-windows/install/)

### Installing 🔧

_Go to root folder_

_Build the images running docker-compose.yml_

```
docker-compose build
```

_Run the application_

```
docker-compose up
```

_Check if everything is working making a GET Request to http://localhost:4000/api/ping_

_Or click here_

* [PING](http://localhost:4000/api/ping)

_To Test the enpoints, you can access to swaggerUI http://localhost:4000/swagger_

_Or click here_

* [SwaggerUI](http://localhost:4000/swagger)

## Build using 🛠️

_This is my attemp to implement CQRS Pattern with MediatR, real time endpoints using SignalR and a message broker for bot commands responses with RabbitMQ_

* [MediatR](https://github.com/jbogard/MediatR) - Simple Mediator implementation in .NET
* [SignalR](https://dotnet.microsoft.com/apps/aspnet/signalr) - Real-time ASP.NET with SignalR
* [RabbitMQ](https://www.rabbitmq.com/)

---
