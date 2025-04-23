CRISP Blazor
===========
## CRISP
- **C**ommand
- **R**esponse
- **I**nterface-driven
- **S**ervice-oriented
- **P**attern
## Overview

The **CRISP Blazor** is a foundational starting point for building modular, scalable, and maintainable applications. It provides a well-structured architecture with a focus on **CQRS (Command Query Responsibility Segregation)** principles, **clean separation of concerns**, and **extensibility**. This template is designed to streamline the development process by offering pre-defined interfaces, base classes, and patterns for common operations such as CRUD, filtering, paging, and auditing.

## Key Features

1. **CQRS-Based Architecture**:
   - The template separates **commands** (write operations) and **queries** (read operations) for better scalability and maintainability.
   - Includes interfaces and base classes for commands, queries, and responses.

2. **Modular Design**:
   - Encourages modularity by organizing code into modules, making it easier to scale and maintain.
   - Each module encapsulates its own entities, services, and endpoints.

3. **Pre-Defined Service Interfaces**:
   - Provides a rich set of interfaces for common operations:
     - `IRequestService`, `ICreateService`, `IQueryService`, `IFilteredQueryService`, etc.
   - Supports advanced operations like filtering, paging, and archiving.

4. **Extensibility**:
   - Easily extend the template to fit your application's specific needs.
   - Add new modules, entities, or services without disrupting the existing structure.

5. **Auditable Models**:
   - Includes support for auditable entities with `BaseAuditableModel`, enabling tracking of creation, modification, and archival operations.

6. **Clean and Consistent Code**:
   - Follows clean coding principles and enforces consistency across the project.
   - Reduces boilerplate code by leveraging abstract records and generic interfaces.

7. **Pre-Defined Endpoint Interfaces**:
   - Introduced `IEndpoint` interfaces to standardize endpoint definitions across modules.
   - Simplifies the implementation of endpoints by providing a consistent structure.

## Why Use This Template?

1. **Accelerates Development**:
   - Provides a ready-to-use structure for common application patterns, saving time during project setup.
   - Reduces the need to write repetitive boilerplate code.

2. **Improves Maintainability**:
   - Modular design ensures that each feature or domain is self-contained, making it easier to update or refactor.
   - Clear separation of concerns simplifies debugging and testing.

3. **Promotes Best Practices**:
   - Implements CQRS principles, encouraging a clean separation between read and write operations.
   - Encourages the use of interfaces and dependency injection for better testability and flexibility.

4. **Scalable Architecture**:
   - Designed to handle complex applications with multiple modules and features.
   - Supports advanced scenarios like filtering, paging, and auditing out of the box.

5. **Reusable Components**:
   - The pre-defined interfaces and base classes can be reused across multiple projects, ensuring consistency and reducing development effort.

## How It Works

### Core Concepts

1. **Commands**:
   - Represent write operations (e.g., create, update, delete).
   - Example: `CreateCommand`, `ModifyCommand`, `DeleteCommand`.

2. **Queries**:
   - Represent read operations (e.g., fetch single entity, fetch paged results).
   - Example: `SingularQuery`, `FilteredQuery`, `PagedQuery`.

3. **Services**:
   - Encapsulate business logic for handling commands and queries.
   - Example: `IRequestService`, `ICreateService`, `IQueryService`, `IFilteredQueryService`.

4. **Responses**:
   - Standardized response models for queries and operations.
   - Example: `FilteredResponse<T>`.

5. **Auditing**:
   - Built-in support for tracking creation, modification, and archival of entities.

### Example Workflow

1. **Create Operation**:
   - Use `CreateCommand` to define the data required for creating an entity.
   - Implement `ICreateService` to handle the creation logic.

2. **Query Operation**:
   - Use `FilteredQuery<T>` or `PagedQuery<T>` to define the criteria for fetching data.
   - Implement `IFilteredQueryService` to handle the query logic.

3. **Modify Operation**:
   - Use `ModifyCommand` to define the data required for updating an entity.
   - Implement `IRequestService` to handle the update logic.

4. **Delete Operation**:
   - Use `DeleteCommand` to define the data required for deleting an entity.
   - Implement `IRequestService` to handle the deletion logic.

5. **Audit Operation**:
   - Use `ArchiveCommand` to define the data required for archiving an entity.
   - Implement `IRequestService` to handle auditing logic.

## Folder Structure

```
CrispBlazor/
├── CrispBlazor.Client/          # Blazor WebAssembly client-side project
│   ├── Modules/                # Modular structure for client-side features
│   │   ├── ModuleName/         # Example module
│   │   │   ├── Pages/          # Razor pages specific to the module
│   │   │   ├── Components/     # Reusable components for the module
│   │   │   ├── Services/       # API client services for the module
│   │   │   └── Models/         # Module-specific models (DTOs, enums, etc.)
│   ├── Shared/                 # Shared components, services, and models
│   │   ├── Components/         # Shared reusable components
│   │   ├── Services/           # Shared services (e.g., authentication)
│   │   ├── Models/             # Shared models (DTOs, enums, etc.)
│   │   └── Utilities/          # Shared helper classes or extensions
│   ├── App.razor               # Root component
│   ├── Program.cs              # Entry point for the Blazor WASM app
│   └── wwwroot/                # Static assets (CSS, JS, images, etc.)
│
├── CrispBlazor.Server/          # ASP.NET Core server-side project
│   ├── Modules/                # Modular structure for Minimal API
│   │   ├── ModuleName/         # Example module
│   │   │   ├── Module.cs       # Module registration and setup
│   │   │   ├── Endpoints/      # Endpoints for the module
│   │   │   ├── Services/       # Business logic for the module
│   │   │   ├── Models/         # Module-specific entities and models
│   │   │   │   ├── Entity1.cs
│   │   │   │   ├── Entity2.cs
│   │   │   │   └── ...
│   │   │   └── Filters/        # Optional: Custom filters for the module
│   ├── Data/                   # Centralized database-related logic
│   │   ├── Entities/           # Shared or cross-cutting entities
│   │   │   ├── User.cs         # Example shared entity
│   │   │   ├── Role.cs         # Example shared entity
│   │   │   └── ...
│   │   ├── ApplicationDbContext.cs # Central DbContext
│   │   ├── Configurations/     # Entity configurations (Fluent API)
│   │   │   ├── UserConfig.cs
│   │   │   ├── RoleConfig.cs
│   │   │   └── ...
│   │   ├── Migrations/         # EF Core migrations
│   │   └── Seed/               # Optional: Seed data logic
│   ├── Services/               # Application-wide services
│   │   ├── ThirdParty/         # Third-party integrations (e.g., email, SMS)
│   │   │   ├── EmailService.cs
│   │   │   └── ...
│   │   ├── ApplicationServices/ # Core application services
│   │   │   ├── NotificationService.cs
│   │   │   └── ...
│   ├── Program.cs              # Entry point for the server app
│   └── Startup.cs              # Configuration for the server app
│
├── CrispBlazor.Shared/          # Shared project for common code
│   ├── DTOs/                   # Shared Request/Response DTOs
│   ├── Interfaces/             # Shared interfaces for API contracts
│   ├── Models/                 # Shared models (e.g., BaseModel, BaseEntityModel)
│   ├── Enums/                  # Shared enums (e.g., FilterType)
│   ├── Requests/               # Command and Query definitions
│   ├── Responses/              # Standardized response models
│   └── Utilities/              # Shared helper classes or extensions
│
├── CrispBlazor.sln              # Solution file
```

## Getting Started

1. **Clone the Template**:
   - Clone this repository to your local machine.

2. **Configure the Database**:
   - Update the connection string in `appsettings.json`.
   - Run `dotnet ef migrations add InitialCreate` to create the initial database schema.

3. **Add Modules**:
   - Create a new folder under `Modules/` for each feature or domain.
   - Define your entities, services, and endpoints within the module.

4. **Run the Application**:
   - Use `dotnet run` to start the application.
   - Ensure that the `IEndpoint` interfaces are correctly implemented in your modules for seamless API functionality.

## Contributing

Contributions are welcome! If you have ideas for improving this template or encounter any issues, feel free to open an issue or submit a pull request.

## License

This project is licensed under the MIT License. See the `LICENSE` file for details.