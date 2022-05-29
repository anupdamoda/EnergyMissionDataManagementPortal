# Create an Empty project
# Go to startup.cs file

# add .Use defaultfiles() 
add .Use Staticfiles()

remove routing() if already there
remove Endppoint() if alreay there

two ways to run:
Run IIS
or go to Tools > Command Prompt and type "dotnet run" 

Go to Solution and Right click an create a new folder and remane it as wwwroot - the icon will change to web globe

Create index.html inside wwwroot folder

create folders in	-- css for css files
					-- js for javascript 
					inside wwwroot folder

					add references of css path inside index.html
					run and check

To add jquery & bootstrap 
add manage nuget package: Ode To Module
once package is added, 

Go to Solution and Right click and create a npm configuration file
once the npm configuration file is added
inside npm config file: add dependencies jquery and version
inside npm config file: add dependencies of bootstrap and version
save the file -> automatic download -> creation of node modules folder ( if this isnt visible - it would be under the show all files )
reference the jquery path and bootstrap path inside index.html

#### The Static website is ready ####

Before MVC make sure your program.cs and statup.cs are correct.

Now to add MVC (Mode-View-Controller)
add controller download nuget package: Microsoft.AspNetCore.Mvc.Core


Create folders specific names:
Create 'Views' folder -> 1. add index.cshtml, 2. add App folder 3. add contact.cshtml inside App folder
                                              2. create Shared folder 3. Create _Layout.chtml inside Shared folder
											  2. create _ViewStart.cshtml 
											  2. Create _viewImports.cshtml
ViewModels
Controllers
create app controllers

reference controller ( import MVC)
use action Iresult for get and return view()

adding tag helpers - for summary and - for validation

Razor view and Razor pages are different

add
.routing() 
.Endppoint() 
for the MVC to work

################## Entity Framework ###################
* Here we are using the example of Code First mechanism

Create folder 'Data'
create a folder 'Entities' inside folder 'Data'

Create C# files with properties
PRoduct
Order
Order ITems

Download packages:
1. Entity framework sql
2. Entity framework core design

Create ShoeContext 
Go to command prompt and Install dotnet tool

Add a service in startup.cs
related to DBcontext and Run Build

Go back to Command Prompt and Run command 'dotnet ef database update'
there would be an error post running the command

Go to program.cs
Add .ConfigureAppconfiguration( setup configuration)

Create Setup configurtaion method
with builder argument
Iconfiguration

builder.AddJsonfile("config.json", false )
.Add Environmentvariables

Create a config.json file
enter DB connection string details

Build and check 'dotnet ef database update'

once successful, check sql server and DB created

run 'dotnet ef migrations add InitialDB'
** please note if you have to run this in the PowerShell command prompt
run the command as 'PM> add-migration InitialDB'

This generates migrations folder automatically

move folders of migrations to 'Data' folder

run 'dotnet ef database update' again and check tables created in the DB
** please note if you have to run this in the PowerShell command prompt
run the command as 'PM> update-database -verbose'

############ Incase there is a change in the database: Update Database ############
modify 
e.g: to add a column for e.g: a product title /shoe title 
was added in the entity of product and then the corresponding column needs to be added in the 'Product' table

Step 1:
Go to Command Prompt 
and type : dotnet ef add migrations <name>
or dotnet ef migrations add <name>
** please note if you have to run this in the PowerShell command prompt
run the command as 'PM> add-migration <name>'

after this a new ,cs file should get generated in migrations folder

Step2:
In Command prompt run 'dotnet ef database update'
** please note if you have to run this in the PowerShell command prompt
run the command as 'PM> update-database -verbose'

Repository Pattern
This should be creating a new class called as ShoeRepository, which has certian methods which returns differet static data

These methods have job to expose different calls to database

Extract Interface from Repository

add the service in Startup 

######Incase of failures during migration#####
Verify if the failure is due to trying to create - already existing tables or its trying to add/drop columns to non-existent table
solution that will help: delete the Migrations folder and delete the dbo._EFMigrationsHistory table 
after that run the above commands of add and update


# APIs

Install Postman
Create a new controller under controller folder
GET 
POST

we can add validations at API Level Similar to the validations at view level

Go to 'View Model' folder and Create an Order View model 
add restrictions as needed in this View model

Automapper

Go to Depdencies in Solution -> Right Click manage Nuget Packages 
Automapper.Extensions, Microsoft Dependency Injection

Add the above package
After tge PAckage is added
go to startup.cs
add as a service of Automapper and using system.Reflections
Assembly.Get ExcutingAssembly()
Create one more service in the Orders Controllers

Add mapper in OrdersController

-mapper.Map<order, orderViewModel>

Go to Data folder and create a new .cs class and make it as Shoemapping Profile which derivces from "profile" class
Create Map maps

############ Identity Core: #####################

1. Authorize attribute
2. Configuring Identity
3. Storing Identity with Entity framework
4. Using Identity in API

# Step 1:
Adding Authorize attribute in App Controller for Shop()

# Step2:
Go to Data folder and then to Entities folder 
Create a cs file called as store user deriving from class Identity User
add properties that are needed for Identities

# Step 3: 
Go to Context Class and replace DBContext base class with Identity DB Context

Add Nuget Package: 'Microsoft AspNetCore.Identity.Framework Core'
that should fix the errors.

# Step4: Go to Data -> Enitities 
and go to Order Class and a new property called as StoreUser.

# Step5: Open CLI Tool and add migrations

run command: dotnet ef migrations add Identity 

Go to migrations folder and verify if the new file got generated with _identity.cs file

_identity.cs file contains the code which makes changes to order tables and identity 

Go to CLI Tools again and run command: 'dotnet ef database drop' -- very Important Step !!
But please note that this command will delete the whole database Schema along with the data 
next time application runs it will populate the new tables related to identity & order related table will be containing identity related details

you will have to run the migration commands again:
1. dotnet ef migrations add <name>
2. dotnet ef database update

when you run the above commands:
new tables related to identity will get generated:
dbo.AspNetUsers, dbo.AspNetRoles, dbo.AspNetUserClaims, dbo.AspNetUser logins and other related tables

# Step6: 
Go to Data folder and go to Seeder method add the details about create Userand in the product method as well

wouyld have to change the Seeder method from Sync to Async

# Step7:
Go to CLI and run command : dotnet ef database drop
and then run /seed

#Step8: 
Go to startup.cs
at the beginning of the configure service
1. Add services.AddIdentity and parameters will be StoreUser & Identity Role
2. fluid syntax .AddEntityFramework Stores with parameter Shoe Context
3. In Configure add use authentication, use authorization below Routing and above endpoints

#Step9:
Design a Login View
1. Goto Controller folder
Add AccountController
Add an 'IActionResult' which can be used to return a view.
2. Go to View folder and create a new view called Login.cshtml
Create details of scripts and html details reference
3. Goto View Models folder and create a new ViewModel called as 'LoginView' Model
enter the property details
Username
Password
RememberMe

Go back to View Login.cshtml and update View Model on top

#Step10:
More Secure API
tokens

Go to startup.cs
add the services.AddAuthentication
.AddCookie()
.AddJWTBearer

for JWTBearer token - add manage Nuget Package: Microsoft.ASPNetCore.Authentication.JWTBearer

configure services already has authentication & authorization






