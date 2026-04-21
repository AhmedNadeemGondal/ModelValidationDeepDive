# ModelValidationDeepDive

## Purpose
This repository demonstrates advanced **Model Validation** techniques in **ASP.NET Core Minimal APIs**. It showcases how to move beyond manual `if` checks by utilizing declarative attributes and endpoint filters to ensure data integrity and provide standardized error responses.

## Core Concepts

### 1. Data Annotations
The project uses `System.ComponentModel.DataAnnotations` to define validation rules directly on the `Employee` model.
* **[Required]:** Ensures critical fields like `Name` and `Salary` are present in the request body.
* **[Range]:** Restricts numerical values (e.g., `Salary`) to a specific logical boundary ($50,000 to $200,000$).
* **Custom Attributes:** Implementation of domain-specific logic, such as `[Employee_EnsureSalary]`, to handle complex business rules that standard attributes cannot cover.

### 2. Minimal API Validation Handling
Unlike standard MVC Controllers, Minimal APIs do not automatically validate Data Annotations out of the box. This project utilizes the **MinimalApi.Extensions** library (or Endpoint Filters) to bridge this gap.

* **.WithParameterValidation():** An extension method that intercepts the request, triggers the validation engine, and automatically returns a `400 Bad Request` with a detailed problem specification if any rules are violated.


### 3. Clean Architecture Implementation
* **Separation of Concerns:** The `EmployeesRepository` manages data persistence in-memory, keeping the API endpoints thin and focused on request handling.
* **Logical Order:** Demonstrates the placement of validation within the middleware pipeline, ensuring that invalid data never reaches the business logic or data store.

### Credits
Credit to **Frank Liu**. Check out his [video series](https://www.youtube.com/watch?v=F4dDe0SLjJM&list=PLgRlicSxjeMOXiYY7deqzO5qKdkg9wrqM&index=1&pp=iAQB) for the original walkthrough.