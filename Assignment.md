# Assignment **10**

For this assignment we will be publishing our web application out to Azure.

## CHANGES FROM PAST ASSIGNMENTS
- The assignment is **DUE BY 11:59pm 3/17**
- This is an individual assignment, we will *not* be pairing on this assignment
- Because this assignment is results driven, we are going to drop the peer review for this assignment
- Because there wont be any peer review or corrections, the assignment will only be worth 40 points

## Assignments
- Sign up for a free Azure account.
- Create Azure Resources
  - A Resource Group.
  - App Service for the API project.
  - App Service for the Web project.
  - SQL server and database.
- Configure App Services with Production environment and relevant connection strings.
- Create a release pipeline that deploys both the API and Web projects.
- Update build pipeline for CI/CD.

## To submit in PR
In the pull request include the following:
- A link to your published API swagger page.
https://apiasauther.azurewebsites.net/swagger/index.html
- A link to your published web site.
https://webasauther.azurewebsites.net/
- A screenshot showing the Azure Resource Group with all resources.
See my headline comment on the pull request
- A screenshot showing the release pipeline that did the deployment.
See my headline comment on the pull request
- Updated build pipeline yaml file.
See [azure-pipelines.yml](azure-pipelines.yml)

## Documentation 
- [https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/azure-apps/?view=aspnetcore-3.1&tabs=visual-studio](Deploy ASP.NET Core apps to Azure App Service)
- [https://docs.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-3.1#set-the-environment](Set environment)
- [https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/providers?tabs=dotnet-core-cli](EF Migrations)