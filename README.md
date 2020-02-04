[![Build Status](https://dev.azure.com/dsergio/CSCD379-2020-Winter/_apis/build/status/dsergio.EWU-CSCD379-2020-Winter?branchName=Assignment4)](https://dev.azure.com/dsergio/CSCD379-2020-Winter/_build/latest?definitionId=1&branchName=Assignment4)


# Assignment 4

For this assignment you will be setting up the SecretSanta.Api project.

- Enable WebAPI analyzers and handle all warnings.

- Setup dependency injection and register all relevant services
  - Ensure all relevant services are registered with appropriate scopes
  - Ensure DB context is registered
  - Ensure AutoMapper is registered

- Apply NSwag to the project and ensure that the swagger page is able to invoke your endpoints. It is ok if invoking the endpoints causes errors due to the use of the SecretSanta.Data classes.

- Setup the Api project with three controllers (GiftController, GroupController, UserController)
  - Each controller should take in a service as a constructor dependency.
  - For now the return types from the controller methods can use the data objects from SecretSanta.Data (we will be fixing this next week).
  - Create CRUD (Create/Read/Update/Delete) endpoints on all of the controllers using appropriate HTTP verbs
  - Where relevant, apply `ProducesResponseTypeAttributes` to the controller methods.

- Unit test the controllers
  - The goal is to simply unit test the code in the controllers. These unit tests should not need to interact with the services.
  - Unit tests should use "test doubles" to satisfy the service dependencies.

## Extra Credit
- There are two commented out tests in EntityServiceTests that are failing due to CreatedBy being overwritten. The solution should either be done inside of the AutoMapper configuration, or inside of the `AddFingerPrinting()` to prevent this value from changing once it has been set. Simply fix the unit tests.
- Creating test doubles can be strealined by using a mocking framework (such as Moq). Rather than writing your own test doubles use a mocking framewokr to create them.

## Relevant Docs
* [S.O.L.I.D.](https://deviq.com/solid/)
* [Dependency Inversion Principle](https://deviq.com/dependency-inversion-principle/)
* [New is glue](https://ardalis.com/new-is-glue)
* [Use web API analyzers](https://docs.microsoft.com/en-us/aspnet/core/web-api/advanced/analyzers)
* [Dependency injection in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection)
* [Controller action return types in ASP.NET Core web API](https://docs.microsoft.com/aspnet/core/web-api/action-return-types)
* [Get started with NSwag and ASP.NET Core](https://docs.microsoft.com/aspnet/core/tutorials/getting-started-with-nswag)
* [Move database initialization code](https://docs.microsoft.com/en-us/aspnet/core/migration/1x-to-2x/?view=aspnetcore-3.1#move-database-initialization-code)
