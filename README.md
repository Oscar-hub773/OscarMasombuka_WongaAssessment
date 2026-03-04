# Wonga Intermediate Software Engineer Assessment [2026-03-04]

## Overview

This project implements a full-stack authentication system as part of the Wonga Intermediate Developer Assessment.

The application allows users to:

* Register a new account
* Log in with existing credentials
* Retrieve their user profile once they are authenticated

The solution is built with a layered architecture, that includes **Unit Testing**, Running on **Docker with PostgreSQL**.

# Architecture

The solution consists of main three  projects:

### Backend

`WongaAssessment.API`

Technologies:

* ASP.NET Core 8 Web API
* Entity Framework Core
* PostgreSQL
* JWT Authentication
* Repository + UnitOfWork pattern
* FluentValidation
* Global Exception Middleware

Responsibilities:

* User registration
* Authentication
* User data retrieval

### Frontend

`WongaAssessment.UI`

Technologies:

* React
* Vite
* React Router
* Context API

Responsibilities:

* Register user
* Login user
* Display authenticated user details


### Tests

`WongaAssessment.Tests`

Technologies:

* xUnit
* Moq
* FluentAssertions

Unit tests cover:

* AuthenticationService
* UserService

# API Endpoints

### Register

POST /api/Authentication/register

### Login

POST /api/Authentication/login

### Get Current User

GET /api/Users/GetCurrentLoggedInUserDetails

FYI : Requires Bearer token authentication.


# Running the Backend

From the API project directory:

dotnet restore
dotnet run

The API will start on: https://localhost:7147


# Running the Frontend

Navigate to the UI project:

cd WongaAssessment.UI
npm install
npm run dev

The frontend will run on: http://localhost:5173

# Running Unit Tests

From the solution root:

dotnet test


# Running with Docker

The application includes a Docker setup using **Docker Compose**.

This will start:

* ASP.NET API container
* PostgreSQL container

Run:

docker compose up --build

The API will be available on:

http://localhost:8080


# Database

The application uses **PostgreSQL** for persistence.

The database schema is managed using **Entity Framework Core migrations**.

# Security

Authentication uses **JWT tokens**.

Passwords are securely hashed before they are stored.

Endpoints require a valid Bearer token.


# Design Considerations

The backend follows a layered architecture:

Controller → Service → Repository → Database


This separation improves:

* testability
* maintainability
* scalability


# Author

Oscar Kagiso Masombuka [2026-03-04]
