# AdventureWorks Bike Store

The AdventureWorks databases are sample databases that were originally published by Microsoft to show how to design a SQL Server database using SQL Server 2008.
AdventureWorks is a fictious manufacturer and reseller of bicycles and related accessories.

This repo holds a range of .NET microservice applications that implement the business domain of AdventureWorks.
The principles of microservice application architecture and Domain Driven Design (DDD) are considered in the implementation.


## Build Status (GitHub Actions)
| Image | Status |
| ------------- | ------------- |
| Basket API | [![basket-api](https://github.com/ngruson/AdventureWorks/actions/workflows/basket-api.yml/badge.svg)](https://github.com/ngruson/AdventureWorks/actions/workflows/basket-api.yml) |
| Customer API | [![customer-api](https://github.com/ngruson/AdventureWorks/actions/workflows/customer-api.yml/badge.svg)](https://github.com/ngruson/AdventureWorks/actions/workflows/customer-api.yml) |
| Identity Server | [![identityserver](https://github.com/ngruson/AdventureWorks/actions/workflows/identityserver.yml/badge.svg)](https://github.com/ngruson/AdventureWorks/actions/workflows/identityserver.yml) |
| Product API | [![product-api](https://github.com/ngruson/AdventureWorks/actions/workflows/product-api.yml/badge.svg)](https://github.com/ngruson/AdventureWorks/actions/workflows/product-api.yml) |
| Reference Data API | [![reference-data-api](https://github.com/ngruson/AdventureWorks/actions/workflows/reference-data-api.yml/badge.svg)](https://github.com/ngruson/AdventureWorks/actions/workflows/reference-data-api.yml) |
| Sales Order API | [![salesorder-api](https://github.com/ngruson/AdventureWorks/actions/workflows/salesorder-api.yml/badge.svg)](https://github.com/ngruson/AdventureWorks/actions/workflows/salesorder-api.yml) |
| Sales Person API | [![salesperson-api](https://github.com/ngruson/AdventureWorks/actions/workflows/salesperson-api.yml/badge.svg)](https://github.com/ngruson/AdventureWorks/actions/workflows/salesperson-api.yml) |

## Architecture and Design

### Design principles

The principles of microservice application architecture and Domain Driven Design (DDD) are considered in the implementation.

The following microservice architecture principles are taken into account:
- Each microservice is autonomous  and independently deployable
- Each microservice has its own data store
- Service boundaries must be well defined
- A microservice holds its own cache of a subset of data that is owned by another microservice to increase performance and availability
- Interservice communication happens asynchronously as much as possible

The following principles of Domain Driven Design are taken into account:
- The business domain is decomposed into Bounded Contexts (or subdomains)
- The language and terms that describe a bounded context are also used in the application code. This is referred to as Ubiquitous Language.
- 