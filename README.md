# Assignment

The purpose of this assignment is to learn the following:

- Create a data layer using Entity Framework (EF)
- Learn the basics of the EF convention and fluent API ways to configure a database using Code First
- Override save methods to implement fingerprinting of records
- Implement mocking to stub out pieces that haven't been implemented yet
- Unit testing database functionality

## Instructions

1. Add the following classes to the **SecretSanta.Data** project with the following associated properties:
   - **`EntityBase`**
     - `int Id`
   - **`FingerPrintEntityBase`** inherit from **`EntityBase`**
     - `string CreatedBy`
     - `DateTime CreatedOn`
     - `string ModifiedBy`
     - `DateTime ModifiedOn`
   - **`Group`** inherit from **`FingerPrintEntityBase`**
     - `string Name`
   - Create a class that will hold the many to many relationship between User and Group
     - Add a collection on both User and Group that references this class
2. Modify the following classes in the **SecretSanta.Data** project:
   - **`Gift`**
     - Remove the Id property and inherit from FingerPrintEntityBase
   - **`User`**
     - Remove the Id property and inherit from FingerPrintEntityBase
     - Make `Gifts` read/write
     - Create a nullable Santa property that contains a User as someone's Santa
3. Remove non-default constructors for `Gift` and `User`.
4. In the **SecretSanta.Data** project
   - Create the **`ApplicationDbContext`** class
     - Create two constructors
        - One only takes the DbContextOptions and passes it on to the base class
        - One that takes the DbContextOptions and an IHttpContextAccessor, pass the DbContextOptions to the base class and save off the IHttpContextAccessor in a class property
     - Add DbSet properties for User, Gift, and Group classes
     - Override OnModelCreating method and use the fluent API to configure the many-to-many pieces that are needed for setting up the database properly
     - Override both SaveChanges methods and apply the fingerprinting logic
5. Update unit tests in the **`SecretSanta.Data.Tests`** project to test actual database logic instead of just constructor logic
   - Mock up IHttpContextAccessor to verify that fingerprint information is working correctly (using Moq library)


## Extra Credit

The following are options for extra credit:
- Enable console logging that will log all database commands
