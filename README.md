
# UXComex - Order Management (ASP.NET Core MVC + Dapper + SQL Server)

A complete implementation of the technical test: Customers/Products CRUD, Order creation with stock validation and automatic stock decrease, order listing with filters, status updates, and optional notifications.

## Stack
- .NET 6, ASP.NET Core MVC
- SQL Server
- Dapper + Microsoft.Data.SqlClient
- Bootstrap 5 + jQuery (CDN)

## How to run
1. **Create the database**
   - Open SQL Server and run `database/script.sql` (creates DB *UxComexOrdersDb* and seeds data).

2. **Configure the connection string**
   - Edit `WebApp/appsettings.json` if needed. Example for a named instance:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=DESKTOP-2NBDRH3\\ECOMMERCEBD;Database=UxComexOrdersDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
     }
     ```

3. **Restore & run**
   - In the `WebApp` folder:
     ```bash
     dotnet restore
     dotnet run
     ```
   - Open the printed URL (e.g., `https://localhost:7143`).

## Features
- Customers: CRUD + search by name/email
- Products: CRUD + search by name
- Orders: create (with dynamic items via jQuery, total auto-calc), list + filters (customer/status), details with items, update status (New/Processing/Finished)
- Stock validation on server (and stock decrease on create)
- Notifications table records each status change

## Notes
- Code is written in English, clean and commented where needed.
- No scaffolding used.
- Simple layered structure with repositories (Dapper) and DI.

Good luck!
