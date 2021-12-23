# .Net Core Microservices

## Technics

### Devops

- Azure devops
- Github
- Azure kubernetes service (AKS)
- Azure container registry (ACR)
- Docker / Docker-compose

### Databases

- MongoDB
- Redis
- Postgres
- SqlServer
- Elasticsearch
-

### Queue

- RabbitMQ
- MassTransit

### Monitoring

- pgadmin
- portainer
- kibana
- watchdog

### Code / Tricks

- ASP NETCORE 5 [*]
- CQRS [Ordering.API]
- Mediator [Ordering.API]
- AutoMapper [*]
- Serilog [*]
- Razor / MVC [AspnetRunBasics]
- Ocelot [ApiGateway]
- Aggregator [Shopping.Aggregator]
- MassTransit [Order.API,Basket.API]
- Polly [*]
- Watch Dog [WebStatus]
- Grpc [Basket.API,Discount.Grpc]
- Identity Server [Identity.API]

## Testing

## Authentication / Authorization

### Docker

```
$ docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
```

### TEST URLs

- Catalog API -> http://host.docker.internal:8000/swagger/index.html
- Basket API -> http://host.docker.internal:8001/swagger/index.html
- Discount API -> http://host.docker.internal:8002/swagger/index.html
- Ordering API -> http://host.docker.internal:8004/swagger/index.html
- Shopping.Aggregator -> http://host.docker.internal:8005/swagger/index.html
- API Gateway -> http://host.docker.internal:8010/Catalog
- Rabbit Management Dashboard -> http://host.docker.internal:15672 -- guest/guest
- Portainer -> http://host.docker.internal:9000 -- admin/admin1234
- pgAdmin PostgreSQL -> http://host.docker.internal:5050 -- admin@aspnetrun.com/admin1234
- Elasticsearch -> http://host.docker.internal:9200
- Kibana -> http://host.docker.internal:5601/app/home
- Web Status -> http://host.docker.internal:8007
- Web UI -> http://host.docker.internal:8006
- Identity.API Login -> http://host.docker.internal:8009/account/login

## References

- [Microservices Architecture and Implementation on .NET 5](https://www.udemy.com/course/microservices-architecture-and-implementation-on-dotnet/)

- [Microservices Observability, Resilience, Monitoring on .Net](https://www.udemy.com/course/microservices-observability-resilience-monitoring-on-net/)

- [SingletonSean/authentication-server](https://github.com/SingletonSean/authentication-server)

- [aspnetrun](https://github.com/aspnetrun)

- [AspnetMicroservices_CrossCutting](https://github.com/mehmetozkaya/AspnetMicroservices_CrossCutting)

- [ASP.NET Core JWT Authentication Server](https://www.youtube.com/playlist?list=PLA8ZIAm2I03hG7cAQC6xytRanKLbS7fTK)

- [run-aspnetcore-microservices](https://github.com/aspnetrun/run-aspnetcore-microservices)

- [Polly](https://github.com/App-vNext/Polly)

- [Architecting Cloud Native .NET Applications for Azure](https://docs.microsoft.com/en-us/dotnet/architecture/cloud-native/)

- [AspNetCore.Diagnostics.HealthChecks](https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks)
