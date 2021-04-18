CVitae

By Michael G. Workman

Azure Cloud Platform Generic URL: https://cvitae20181226024341.azurewebsites.net/

Permanent URL: http://www.michaelgworkman.com

First version of CV Model View Controller (MVC) web application which will be on domain michaelgworkman.com

This MVC web application has been implemented with database first Entity Framework,
but is different than a normal MVC web application in that it uses GUID for primary key ID in EF models and database tables,
instead of an INT with IDENTITY. Also this MVC web application does not use bootstrap CSS, but uses W3Schools CSS styles instead,
available at https://www.w3schools.com/w3css/.

Before running this solution, use the CV_Create.sql TSQL script in the SQL SERVER repo to create the CVDB sql server database used by this solution.

To create the database on Azure Cloud Platform, first create the database in the Azure portal and run the create script with the create database statement commented out or removed in CV_Create.sql TSQL script in SQL Server Repo.

This solution works with LocalDB sql server, but changing the CVitaeContext connection string in Web.Config file will enable you to use it with any other sql server database. In this repo it has been configured to work with Azure Cloud Platform SQL Server, after the first version was set to use LocalDB.

This application is freely distributable under terms of MIT open source license.

(c) Copyright Michael G. Workman
