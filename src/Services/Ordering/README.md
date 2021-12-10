### Projects

- Ordering.API

     - ref

        - Ordering.Application

        - Ordering.Infrastructure

- Ordering.Application 

    - ref

        - Ordering.Domain

- Ordering.Domain

- Ordering.Infrastructure

     - ref

        - Ordering.Application

#### Database

- setup

      $ docker pull mcr.microsoft.com/mssql/server:2017-latest

- run

      $ docker run -d -p 1433:1433 --name orderdb mcr.microsoft.com/mssql/server:2017-latest

- check
      
      $ docker logs orderdb -f

- exec

      $ docker exec -it orderdb /bin/bash

- command

      # 

- gui 

      
