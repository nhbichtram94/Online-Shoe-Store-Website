# Source Code

This folder contains the source code of the **Online Shoe Store Website**, developed using **ASP.NET MVC**, **Entity Framework**, and **SQL Server**.

## Project Structure

```
src/
├── Controllers/
├── Models/
├── Views/
├── Content/
├── Scripts/
├── App_Start/
└── Properties/
```

## Architecture

The application follows the **Model–View–Controller (MVC)** architecture.

- **Models**: Define business entities and interact with the SQL Server database through Entity Framework.
- **Views**: Implement the user interface using Razor (.cshtml), HTML, CSS, and Bootstrap.
- **Controllers**: Handle user requests, process business logic, and coordinate data between models and views.

## Main Functional Modules

### Customer

- User Registration
- User Login
- Product Browsing
- Product Search
- Product Details
- Shopping Cart
- Checkout
- Order History

### Administrator

- Category Management
- Product Management
- Order Management

## Technologies

- ASP.NET MVC
- C#
- Entity Framework
- SQL Server
- Bootstrap
- HTML5
- CSS3
- JavaScript

## Running the Project

### Requirements

- Visual Studio 2022 (or later)
- SQL Server
- .NET Framework

### Steps

1. Clone the repository.
2. Restore NuGet packages.
3. Create the database using the provided SQL script.
4. Update the database connection string in `Web.config`.
5. Run the project with IIS Express.

## Notes

This repository presents my contribution to a collaborative course project developed for the Web Programming course.
