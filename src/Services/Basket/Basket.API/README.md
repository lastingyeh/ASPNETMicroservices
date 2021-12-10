### Basket.API

---
### Structure

- webapi architecture (folder)

- dependencies

	- Microsoft.Extensions.Caching.StackExchangeRedis

      - Grpc.AspNetCore

- connections

      - Discount.Grpc

- implements

    - repositories

        - GetBasket

        - UpdateBasket

        - DeleteBasket
        
---
#### Database

- setup

      $ docker pull redis

- run

      $ docker run -d -p 6379:6379 --name asprun-redis redis

- check
      
      $ docker logs asprun-redis -f

- exec

      $ docker exec -it asprun-redis /bin/bash

- command

      # redis-cli
      
      > ping

      > set key value

      > get key

      > set name api

      > get name  