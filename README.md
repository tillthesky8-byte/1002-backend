# 1002-backend v1.0.1
This is the backend project for one-user 1002-frontend application. It provides RESTful API for the frontend to interact with the database. The project is built with ASP.NET Core and uses SQLite as the database. Dapper is used for data access.

The application consists of three main domains: DreamEntries, Survey, and Todos. Each domain maps to its own repository, service, and controller.

## Tech Stack
- .NET 10
- C# 14
- ASP.NET Core 10
- SQLite
- Dapper (raw SQL, micro ORM)
- NuGet

## Architecture

The project follows a layered architecture with clear separation of concerns:

- **Controllers** — handle HTTP requests and responses, no business logic
- **Services** — encapsulate business logic, depend on repositories via interfaces
- **Repositories** — all data access logic, raw SQL via Dapper, no business logic
- **Models** — plain C# classes representing database entities

Dependencies flow strictly downward: Controllers → Services → Repositories.
All cross-layer dependencies are abstracted through interfaces and resolved via dependency injection.

## Description
The initial version of the project includes the following features:

- Endpoints for managing todo feature with basic CRUD operations, and custom endpoints for marking todos as completed or failed, retrieving all todos.

- Endpoints for managing survey feature only with custom endpoints for submitting survey responses and checking if a user has already submitted a survey for the day.

- Endpoints for managing dream entries with basic CRUD operations, and custom endpoints for retrieving all dream entries and filtering them by date.

- Services for handling business logic related to todos:
    - Marking a todo as completed or failed
    - Retrieving all todos for a user

- Services for handling business logic related to surveys:
    - Submitting survey responses
    - Checking if a user has already submitted a survey for the day

- Services for handling business logic related to dream entries feature:
    - Retrieving all dream entries for a user
    - Filtering dream entries by date

- Repositories for operations with data access related to todos, surveys, and dream entries features.


