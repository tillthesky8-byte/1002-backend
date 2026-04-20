# CHANGELOG
## [Unreleased] `v1.0.3` 1002-backend
### Changes for 2026-04-23
- Create specialized agents for Repository level. Define general protocol, specialized protocol extensions for different types of operations, and a bootstrap template for creating new repository files.
    - *Repository Layer.*

## [Unreleased] `v1.0.2` 1002-backend 
### Changes for 2026-04-19
- Add flexible patch update methods for domains.
    - *Repository Layer, Service Layer, Controller Layer.*

- Complete MarkTodoAsFinished method in TodoService and corresponding endpoint in TodoController. Using patch update method
    - *Service Layer, Controller Layer.*

- Create special Answer model for survey submission request without Id and SurveySessionId properties.
    - *Dtos.*


## [Unreleased] `v1.0.1` 1002-backend - 2026-04-19
### Changes for 2026-04-19
<!-- commit message: Fix naming inconsistency in Todo entity -->
- Fix naming inconsistency in Todo entity (`Title` property renamed to `Text` to match database schema and other layers).
    - *Repository Layer, Service Layer, Controller Layer, Models, Dtos.*

- Add more detailed logging in repository methods for better observability.
    - *Repository Layer.*

- Separate logs from each other and other information and make them more structured and readable.
    - *Repository Layer, Service Layer, Controller Layer.*

<!-- commit message: Add TimeFrameId validation in TodoService -->
- TimeFrameId validation in CreateTodo and UpdateTodo methods of TodoService, defaulting to 1 if invalid value is provided.
    - *Service Layer.*