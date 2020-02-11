[![Build Status](https://dev.azure.com/jlatimer1/CSCD379-2020-Winter/_apis/build/status/JerettLatimer.EWU-CSCD379-2020-Winter?branchName=Assignment5)](https://dev.azure.com/jlatimer1/CSCD379-2020-Winter/_build/latest?definitionId=1&branchName=Assignment5)

# Assignment

For this assignment you will be completing the SecretSanta.Api project and startng work on the SecretSanta.Web project

- Create DTOs for all entities
- Create "input" DTOs for all entities
- Update AutoMapper configuration to be able to map between relevant DTOs and database entities.
- Update controllers and unit tests to use new DTO objects rather that `SecretSanta.Data` objects.
- Update the SecretSanta.Api project to use a SQLite **file** database.

- Replace the API unit tests for **GiftController** with integration tests
  - The integration tests should should use a SQLite in memory database
  - Seed the database as needed for the tests
  - The integration tests should validate all required parameters. For GiftInput both Title, and UserId should be required.

- Generate C# client for the SecretSanta.Web project
  - Generated code should be in the `SecretSanta.Web.Api` namespace
  - Generated clients should have generated interfaces

- In the SecreateSanta.Web project create basic MVC controllers 
  - Controllers should use the generated clients to invoke the API
  - Controllers should have a single index endpoint that simply displays a list of items for each of the content types
  - Code is this project **does not** need to be tested for this assignment

## Extra Credit
- Implement the integration tests for the UserController, and GroupController.

## Relevant 
- [Integration Testing](https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-3.1)
- [Create Data Transfer Objects (DTOs)](https://docs.microsoft.com/en-us/aspnet/web-api/overview/data/using-web-api-with-entity-framework/part-5)
- [NSwag Code Generation](https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-nswag?view=aspnetcore-3.1&tabs=visual-studio#code-generation)
- [SQLite Connection Strings](https://docs.microsoft.com/en-us/dotnet/standard/data/sqlite/connection-strings)
- [Implementing Disposable](https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose)
- [ReadWrite Json](https://docs.microsoft.com/en-us/dotnet/standard/serialization/system-text-json-how-to)
