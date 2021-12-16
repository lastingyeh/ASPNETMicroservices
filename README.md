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

### Monitoring
- pgadmin
- portainer
- kibana

### Code / Code structure
- net core 5.0
- CQRS
- Mediator
- AutoMapper
- Serilog
- Razor / MVC
- Ocelot (ApiGateway)
- Aggregator (Backend for Frontend)
- MassTransit (RabbitMQ)
  
## Testing
---
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

## References
---

- [Microservices Architecture and Implementation on .NET 5](https://www.udemy.com/course/microservices-architecture-and-implementation-on-dotnet/)

- [SingletonSean/authentication-server](https://github.com/SingletonSean/authentication-server)

- [ASP.NET Core JWT Authentication Server](https://www.youtube.com/playlist?list=PLA8ZIAm2I03hG7cAQC6xytRanKLbS7fTK)

- [AspnetMicroservices_CrossCutting](https://github.com/mehmetozkaya/AspnetMicroservices_CrossCutting)

- [run-aspnetcore-microservices](https://github.com/aspnetrun/run-aspnetcore-microservices)