# Assignment

- Within the API project
  - Remove the hardcoded Data Source value for the database and use the value stored in the ConnectionStrings.DefaultConnection within the appropriate App Settings file ✔
  - Replace the EnsureCreated with a call to Migrate ✔
    - This will require the generation of Migrations as well. The command should be run from the API project, but the migrations should live within the Data project ✔
  - Modify the ConfigureServices and Configure methods to use the new ASP.NET Core 3.x routing ✔

- Within the Web project
  - Modify the ConfigureServices and Configure methods to use the new ASP.NET Core 3.x routing ✔
  - Set the BaseAddress of the HttpClient so the value is coming from the appropriate App Settings file ✔❌
  - Regenerate the Client.g.cs file with the following settings ✔
    - Generated code should be in the `SecretSanta.Web.Api` namespace
    - Generated clients should have generated interfaces
    - Generated code should not use the base url for the request
  - Create an _Layout.cshtml file and put the "Chrome" for the application within it (this should include the navigation that allows one to get Home, Users, Gifts, Groups) ✔ (In progress - Layout created, needs to be worked on)
    - All pages created should use this _Layouts file
  - Create Create, Edit, Delete pages for Users, Gifts, Groups ✔
    - Display validation errors if any occur (server side validation)
  - Enable TagHelpers functionality and move all namespaces into the _ViewImports file ✔
  - Configure webpack so that all style assets get created and copied into the wwwroot folder ✔❌
    - Should auto-generate the _Layouts.cshtml from the _LayoutsTemplate.cshtml file
    - Should have bulma added as the css framework and using the scss version of the assets

## Extra Credit
- Add client side validation of fields ✔❌
