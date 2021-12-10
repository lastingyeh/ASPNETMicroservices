### Discount.API

---
### Structure

- webapi architecture (folder)

- dependencies

	- Npgsql

      - Dapper

---
#### Database

- setup

      $ docker pull postgres

- run

      $ docker run -d -p 5432:5432 --name discount-pg postgres

- check
      
      $ docker logs discount-pg -f

- exec

      $ docker exec -it discount-pg /bin/bash

- command

      # 

- gui 

      $ docker pull dpage/pgadmin4

      > http://localhost:5050
      
   