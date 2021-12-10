## Catalog.API
---
### Structure

- webapi architecture (folder)

- dependencies

	- Microsoft.Extensions.Caching.StackExchangeRedis

- implements

    - repositories

        - GetProducts

        - GetProduct

        - GetProductsByName

		- GetProductsByCatalog

		- CreateProduct

		- UpdateProduct

		- DeleteProduct
---
### Database

- setup 
	
	  $ docker pull mongo

- run

	  $ docker run -d -p 27017:27017 --name shopping-mongo mongo 

- check

	  $ docker logs shopping-mongo -f 

- exec 

	  $ docker exec -it shopping-mongo /bin/bash

- command (bash)

      # mongo

	  > show dbs

	  > use CatalogDb
			
	  > show dbs

	  > db.createCollection('Products')

	  > show collections
			
	  > db.Products.insertMany([{ 'Name':'Asus Laptop','Category':'Computers', 'Summary':'Summary', 'Description':'Description', 'ImageFile':'ImageFile', 'Price':54.93 }, { 'Name':'HP Laptop','Category':'Computers', 'Summary':'Summary', 'Description':'Description', 'ImageFile':'ImageFile', 'Price':88.93 }])

	  > db.Products.find({}).pretty()

	  > db.Products.remove({})
	
- [mongoclinet](https://hub.docker.com/r/mongoclient/mongoclient)

	  $ docker run -d -p 3000:3000 mongoclient/mongoclient

---
### docker-compose 

	  $ docker-compose -f docker-compose.yml -f docker-compose.override.yml up --build



