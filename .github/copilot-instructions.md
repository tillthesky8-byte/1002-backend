# Project Overview
This is a backend project built with ASP.NET Core. The original goal of this project is to provide RESTful API for 1002-fontend application. The project doesn't use Entity Framework, so all the queries are going to be written in raw SQL to a SQLite database.

Basically it provides three main features: todo, survey, and dream entries. Each feature has its own controller, service, repository, and set of database tables.

# Project Rules
The project must be structured in a way that allows for easy maintenance and scalability. The simple set of principles that we will follow are:
1. Clear separations of layers.
2. Use of interfaces to abstract away implementation details.
3. Use of dependency injection to manage dependencies between layers.
4. Use of repositories to abstract away methods with data access logic.
5. Use of services to encapsulate business logic.
6. Use of controllers to handle HTTP requests and responses.
7. The nodes should have minimal connections to each other.

## Other smaller conventions:
- Use PascalCase for class names, method names, and properties.
- Use camelCase for local variables and method parameters.
- Use clear and descriptive names for classes, methods, properties, and variables.
- Keep methods short and focused on a single task.
- Avoid code duplication by extracting common logic into reusable methods or classes.
- Use comments to explain complex logic or decisions, but avoid obvious comments that do not add value

# Technologies Used
- .NET 10
- C# 14
- ASP.NET Core 10
- SQLite
- Dapper 
- NuGet

# Folder Structure
Controllers/
  - DreamEntriesController.cs
  - SurveyController.cs
  - TodoController.cs

Services/
  - DreamEntryService.cs
  - SurveyService.cs
  - TodoService.cs

Repositories/
  - DreamEntryRepository.cs
  - SurveyRepository.cs
  - TodoRepository.cs

Models/
  - DreamEntry.cs
  - Question.cs
  - QuestionType.cs
  - Section.cs
  - Answer.cs
  - SurveySession.cs
  - Todo.cs
  - TodoStatus.cs
  - TodoTimeFrame.cs


# Data Access Rules
- Use Dapper for all database access
- No EF Core, no LINQ-to-SQL
- All SQL is written as raw hardcoded strings inside repository methods
- All queries/transactions belong in Repository classes only, never in Services or Controllers
- Use parameterized queries/transactions always (@Param syntax) — never string concatenation

# Workflow
1. .docs/plan.md: This file contains the project implementation plan and commit messages. Which must be followed when implementing the project. The commit messages must be clear and concise, and must follow the format specified in the file. 

2. .docs/notes.md: This file contains notes and thoughts about the project implementation. Can be ignored by the agent, but can be used as a reference for the implementation.

# Database Schema
app.db:
`tables`/
    - `Answers`/
        - columns/
            - Id (INTEGER, PK)
            - QuestionId (INTEGER, FK to Questions)
            - SurveySessionId (INTEGER, FK to SurveySessions)
            - Response (TEXT)
            - Remark (TEXT)
        - indexes/
            - idx_answers_surveysessionid_questionid (SurveySessionId, QuestionId)
    - `DreamEntries`/
        - columns/
            - Id (INTEGER, PK)
            - Title (TEXT)
            - Description (TEXT)
            - Date (INTEGER)
    - `Questions`/
        - columns/
            - Id (INTEGER, PK)
            - Code (TEXT UNIQUE NOT NULL)
            - Text (TEXT)
            - SectionId (INTEGER, FK to Sections)
            - QuestionTypeId (INTEGER, FK to QuestionTypes)
        - indexes/
            - idx_questions_sectionid_questiontypeid (SectionId, QuestionTypeId)
    - `QuestionTypes`/
        - columns/
            - Id (INTEGER, PK)
            - Name (TEXT UNIQUE NOT NULL)
    - `Sections`/
        - columns/
            - Id (INTEGER, PK)
            - Name (TEXT)
    - `SurveySessions`/
        - columns/
            - Id (INTEGER, PK)
            - Date (TEXT UNIQUE NOT NULL)
    - `TodoStatuses`/
        - columns/
            - Id (INTEGER, PK)
            - Name (TEXT NOT NULL UNIQUE)
    - `TodoTimeFrames`/
        - columns/
            - Id (INTEGER, PK)
            - Name (TEXT NOT NULL UNIQUE)
    - `Todos`/
        - columns/
            - Id (INTEGER, PK)
            - Text (TEXT)
            - CreatedAt (INTEGER)
            - DueAt (INTEGER)
            - FinishedAt (INTEGER)
            - StatusId (INTEGER, FK to TodoStatuses)
            - TimeFrameId (INTEGER, FK to TodoTimeFrames)