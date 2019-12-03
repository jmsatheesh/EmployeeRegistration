# EmployeeRegistration
Initial Commit

This is a simple .Net core application for perform CRUD operations for a employee registration form.


Features Included
1. .Net Core Web Api for CRUD operation
2. ASP.Net MVC Core application to For UI
3. EF Core for DB operations
4. Generic Repository to handle all the CRUD operations 
5. Exception Handling
6. Swagger specification included to Web API

Featues not included due to time and Resources constrains
1. CQRS Design Pattern - I already implemented this concept in my project. Need to create 2 database for read an write operation. Need to create 2 different dbcontext. The database sync up need to handle by code or Db replication with a time duration. By code we can summamarise the files and create a new tables for reporting purpose. With the fixed time slot we can push the records from transactional db to Reporting DB.
2. Common Exception handling
3. Azure hosting

Azure setup
1. Create  Web Apps in the Azure 
2. Create a database in the Azure
3. Change the connection string in the web api application settings
4. Run the Update-Database command from web api application from VS editor
5. Publish the Web API and MVC application as two different application in to the web apps

Normal Setup
1. Change the connection string in the web api application settings
2. Run the Update-Database command from web api application from VS editor
