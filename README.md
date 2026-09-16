# FitnessProjectTeam4

FitnessProjectTeam4 is a backend for a fitness and wellness application. It separates identity, fitness calculations, nutrition content, profiles, workouts, and progress tracking into focused ASP.NET Core services.

> **Current state:** This repository is under active development. Several services contain development-only identity stubs or test user IDs; see [Authentication and security](#authentication-and-security) before treating the APIs as production-ready.

## Contents

- [Architecture](#architecture)
- [Services and shared projects](#services-and-shared-projects)
- [Technology stack](#technology-stack)
- [Implemented capabilities](#implemented-capabilities)
- [Repository layout](#repository-layout)
- [Prerequisites](#prerequisites)
- [Configuration](#configuration)
- [Database migrations](#database-migrations)
- [Run locally](#run-locally)
- [API documentation and example requests](#api-documentation-and-example-requests)
- [Authentication and security](#authentication-and-security)
- [Messaging and observability](#messaging-and-observability)
- [Development notes](#development-notes)
- [Known gaps](#known-gaps)

## Architecture

The repository uses a microservice-oriented design. Each service owns an Entity Framework Core `DbContext` and SQL Server schema, exposes HTTP endpoints, and organizes business operations as MediatR commands and queries. RabbitMQ/MassTransit is used for selected asynchronous service interactions.

```text
                         +----------------+
                         |  Auth Service  |
                         | JWT, sessions, |
                         | OTP, lockouts  |
                         +----------------+

+-------------------+       RabbitMQ        +-------------------+
| Workout Service   | -- SessionStarted -->  | Progress Service  |
| exercises, plans, |                       | logs, weight,     |
| sessions          | <-- WorkoutProgress --| stats, streaks    |
+-------------------+       Logged          +-------------------+
        |                                             |
        |                                             | weight-update message
        |                                             v
        |                                    +-------------------+
        |                                    | Fitness Calculation|
        |                                    | Engine             |
        |                                    | metrics and plans  |
        |                                    +-------------------+

+-------------------+       request/reply    +-------------------+
| Nutrition Service | -------------------->  | calorie-target    |
| meals and plans   |                        | queue contract    |
+-------------------+                        +-------------------+

+-------------------+
| Profile Service   |
| profile, settings |
| and images        |
+-------------------+
```

The diagram represents the message producers, consumers, and request client present in source. The repository does not include a Docker Compose file, a deployed topology, or a registered responder for the nutrition calorie-target request.

## Services and shared projects

| Project | Responsibility | Target framework |
| --- | --- | --- |
| `AuthService` | Registration, login, JWT/refresh-token sessions, logout, OTP password recovery, and lockout tracking | .NET 8 |
| `FitnessCalculationEngine` | Fitness statistics, BMR/TDEE-derived metrics, plan configuration and assignment, recalculation | .NET 8 |
| `NutritionService` | Meal catalog, meal plans, nutrition facts, and recommendations | .NET 8 |
| `ProfileService` | User profile, settings, and local profile-picture storage | .NET 8 |
| `WorkoutService` | Exercises, workout plans, workouts, and workout-session lifecycle | .NET 9 |
| `ProgressService` | Workout-progress logs, weight history, statistics, and streaks | .NET 9 |
| `SharedMessages` | Workout/Progress message contracts and queue names | .NET 9 |
| `NutritionSharedMessages` | Nutrition calorie-target request/reply contracts | .NET 8 |

The root `FitnessProjectTeam4.csproj` is a default ASP.NET Core template and is not the application entry point for the service system. `FitnessProjectTeam4.slnx` includes Fitness Calculation Engine, Nutrition, Progress, Workout, and both shared-contract projects; Auth and Profile must currently be opened or run as individual projects.

## Technology stack

- ASP.NET Core Web API: Minimal APIs and MVC controllers
- C# with .NET 8 and .NET 9
- SQL Server and Entity Framework Core, with migrations per service
- MediatR for command/query dispatch
- FluentValidation for validation behaviors
- AutoMapper in Profile, Workout, and Progress
- MassTransit and RabbitMQ for messaging
- Entity Framework outbox integration in Workout and Progress
- Swashbuckle/OpenAPI for development Swagger UIs
- BCrypt.Net for password hashing and MailKit for SMTP email in Auth
- Serilog, Console, and Seq sinks in Workout and Progress
- Multi-stage Dockerfiles for Nutrition, Profile, Workout, and Progress

## Implemented capabilities

### Identity and account recovery

- User registration and profile-completion event publishing
- JWT access tokens, refresh tokens, and logout
- BCrypt password hashing
- OTP-based password reset flow
- Login-attempt recording, temporary lockouts, and periodic purge of expired attempts
- SMTP email sender with a console-email fallback when SMTP is not configured
- Fixed-window rate limits for login and password-reset initiation

### Fitness, nutrition, and profile

- Fitness-stat submission: weight, height, age, gender, goal, and activity level
- Fitness metric calculation, retrieval, recalculation, and plan assignment
- Paginated fitness-plan configuration lookup
- Seeded nutrition catalog with meal plans, meal details, calorie-range filtering, and recommendations
- Profile retrieval and updates; notification, privacy, and preference settings
- Local profile-picture upload and static-file serving

### Workouts and progress

- Exercise creation, filtering, pagination, and detail retrieval
- Workout-plan and workout creation, browsing, and filtering
- Adding exercises to workouts and starting workout sessions
- Logging completed workouts, exercise completion, ratings, notes, calories, and duration
- Weight logging/history, aggregate progress, user statistics, and streaks
- Asynchronous session-start and workout-completion processing

## Repository layout

```text
AuthService/                 Authentication API and persistence
FitnessCalculationEngine/    Fitness metrics, plans, and recalculation API
NutritionService/            Meal, meal-plan, and recommendation API
NutritionSharedMessages/     Nutrition request/reply contracts
ProfileService/              Profile, settings, and image API
ProgressService/             Progress API, message consumer, and outbox
SharedMessages/              Workout/progress event contracts and queues
WorkoutService/              Workout API, message consumer, and outbox
FitnessProjectTeam4.slnx     Partial solution definition
```

Within services, `Domain` contains entities and value types; `Features` contains commands, queries, handlers, DTOs, validators, and endpoint/controller code; `Common` and `Infrastructure` contain cross-cutting concerns, persistence, messaging, and API response/error handling.

## Prerequisites

- .NET SDK 8.x and .NET SDK 9.x
- SQL Server accessible to each service
- RabbitMQ for Auth, Fitness Calculation Engine, Nutrition, Workout, and Progress message features
- Optional: Seq for the configured Workout and Progress logging sink
- Optional: Docker for services that include Dockerfiles

No container orchestration definition is included. Provision infrastructure independently and set service configuration before running services.

## Configuration

Each service loads standard ASP.NET Core configuration from its `appsettings.json`, environment-specific settings, environment variables, and (where configured) User Secrets. Never commit real connection strings, JWT signing keys, SMTP credentials, or broker credentials.

Configuration keys used by the code include:

| Service | Required or relevant configuration sections |
| --- | --- |
| Auth | `ConnectionStrings:AuthDb`, `Jwt`, `Smtp`, `RabbitMq`, `Lockout`, `Otp` |
| Fitness Calculation Engine | `ConnectionStrings:FceDb`, `RabbitMq` |
| Nutrition | `ConnectionStrings:DefaultConnection` |
| Profile | `ConnectionStrings:DefaultConnection` |
| Workout | `ConnectionStrings:DefaultConnection`, `Serilog` |
| Progress | `ConnectionStrings:DefaultConnection`, `Serilog` |

For local development, environment variables can supply sensitive Auth configuration without adding it to source-controlled JSON files:

```powershell
$env:ConnectionStrings__AuthDb = '<SQL_SERVER_CONNECTION_STRING>'
$env:Jwt__Key = '<AT_LEAST_32_CHARACTER_SIGNING_KEY>'
```

Projects that declare a `UserSecretsId` can also use `dotnet user-secrets` for local values. The Auth service validates that `Jwt:Key` is present and at least 32 characters long. Configure `Jwt:Issuer`, `Jwt:Audience`, token lifetimes, and SMTP/RabbitMQ credentials in the same secure manner. Do not copy secrets into this README or source-controlled configuration files.

## Database migrations

Each data-owning service contains EF Core migrations. After configuring its SQL Server connection, apply migrations from the repository root:

```powershell
dotnet ef database update --project AuthService
dotnet ef database update --project FitnessCalculationEngine
dotnet ef database update --project NutritionService
dotnet ef database update --project ProfileService
dotnet ef database update --project WorkoutService
dotnet ef database update --project ProgressService
```

Nutrition seeds its catalog when the application starts. Review the service migration history and connection target carefully before applying migrations to a shared or production database.

## Run locally

Restore packages and run each desired API in a separate terminal:

```powershell
dotnet restore FitnessCalculationEngine/FitnessCalculationEngine.csproj

dotnet run --project AuthService
dotnet run --project FitnessCalculationEngine
dotnet run --project NutritionService
dotnet run --project ProfileService
dotnet run --project WorkoutService
dotnet run --project ProgressService
```

The committed launch profiles define these development HTTP ports:

| Service | HTTP port |
| --- | --- |
| Auth | 5001 |
| Fitness Calculation Engine | 5166 |
| Nutrition | 5238 |
| Profile | 5113 |
| Workout | 5112 |
| Progress | 5116 |

The APIs redirect HTTP to HTTPS where configured. Consult each service's `Properties/launchSettings.json` for its available local profiles.

## API documentation and example requests

Swagger UI is enabled only in the Development environment. For services with a Swagger launch profile, browse to:

```text
http://localhost:<service-port>/swagger
```

Auth and Fitness Calculation Engine use Minimal APIs; Nutrition also maps endpoints through its endpoint-definition abstraction. Profile, Workout, and Progress use MVC controllers.

Examples, assuming the corresponding local service is running:

```powershell
# Register an account
Invoke-RestMethod -Method Post http://localhost:5001/api/v1/auth/register `
  -ContentType 'application/json' `
  -Body '{"firstName":"Ada","lastName":"Lovelace","email":"ada@example.test","password":"<password>","phoneNumber":"<phone>"}'

# Browse nutrition meal plans (authorization is required by the endpoint)
Invoke-RestMethod http://localhost:5238/api/v1/nutrition/meal-plans?pageIndex=1&pageSize=10 `
  -Headers @{ Authorization = 'Bearer <access-token>' }

# Retrieve exercises
Invoke-RestMethod 'http://localhost:5112/api/v1/exercises?page=1&pageSize=10'
```

Key endpoint groups:

| Service | Route prefix |
| --- | --- |
| Auth | `/api/v1/auth` |
| Fitness Calculation Engine | `/api/v1/fitness` |
| Nutrition | `/api/v1/nutrition` |
| Profile | `/api/v1/profile`, `/api/v1/settings` |
| Workout | `/api/v1/exercises`, `/api/v1/workouts` |
| Progress | `/api/v1/progress` |

Auth and Fitness Calculation Engine also map a `/health` endpoint.

## Authentication and security

`AuthService` implements JWT bearer authentication with issuer, audience, signing-key, and lifetime validation. It uses BCrypt for passwords, hashed OTPs for recovery, refresh-token family tracking, lockout policy configuration, and rate limiting for anonymous login/recovery requests.

Current authorization is **not consistently production-configured across all services**:

- Fitness Calculation Engine requires authorization on its feature routes, but its registered `Dev` authentication handler always supplies a fixed development identity.
- Nutrition marks its routes as requiring authorization, but its JWT setup is commented out in `Program.cs`.
- Profile currently registers `MockCurrentUserService`; its profile and settings controllers are marked anonymous.
- Workout and Progress contain no configured authentication scheme or controller authorization attributes. Their session/progress creation flows contain test user IDs.

Do not rely on these non-Auth services for production access control until authentication is integrated and the development stubs are removed.

## Messaging and observability

RabbitMQ is configured through MassTransit in selected services:

- Workout sends `SessionStartedMessage`; Progress consumes it on the `session-started` queue.
- Progress sends `WorkoutProgressLoggedMessage`; Workout consumes it on the `WorkoutProgress-Logged` queue.
- Progress sends a weight-update message; Fitness Calculation Engine registers a weight-update consumer.
- Nutrition registers a request client for `GetUserCalorieTargetRequest` targeting `fce-calorie-target-service`.
- Auth publishes `UserRegisteredEvent` after profile completion.

Workout and Progress configure MassTransit Entity Framework outboxes. Workout and Progress also configure Serilog for console logging and a Seq sink. Ensure RabbitMQ and optional Seq endpoints are available or adjust configuration/code for the environment.

## Development notes

- Build and run services individually; the checked-in solution does not include Auth or Profile.
- Use the generated Swagger UI in Development to inspect request/response contracts.
- Add or update EF Core migrations in the service that owns the changed data model.
- Keep shared message types in `SharedMessages` or `NutritionSharedMessages`; do not directly share service persistence models.
- No automated test projects or CI workflow files are currently included.

## Known gaps

- There is no Compose/orchestration, deployment, or environment-template file.
- RabbitMQ configuration and message contract alignment should be verified before relying on all cross-service flows end to end. In particular, the Progress weight-update message contract differs from the Fitness Calculation Engine consumer contract in the current source.
- The Nutrition calorie-target request has no responder in this repository.
- Dockerfiles are not available for Auth or Fitness Calculation Engine.
- License, contribution guidance, and a formal production deployment process are not documented in the repository.
