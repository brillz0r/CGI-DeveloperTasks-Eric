# Bookings API

A small ASP.NET Core Web API for managing room bookings. It lets clients create, look up, and cancel bookings for a room over a time range, and rejects any booking that overlaps an existing one for the same room.

The project is intentionally lightweight — bookings are held in an in-memory store, so no database setup is required to run it.

## Features

- List all bookings, fetch a single booking by id, create a booking, and cancel one.
- **Overlap protection** — a new booking is rejected with `409 Conflict` if its time range overlaps an existing booking for the same room. The overlap check and insert are guarded by a lock so concurrent requests can't both slip through.
- **Consistent responses** — services return a `Result` / `Result<T>` object describing the outcome (`Success`, `NotFound`, `ValidationError`, `Unauthorized`, `Conflict`), which the controllers translate into the appropriate HTTP status code, returning RFC 7807 `ProblemDetails` for failures.

## Tech stack

- .NET 8 / ASP.NET Core Web API
- xUnit for tests

## Project structure

```
BookingsApi/
├── API/
│   ├── Controllers/      # BookingsController + BaseController (Result -> HTTP mapping)
│   ├── Services/         # BookingService / IBookingService (business rules, overlap check)
│   ├── Repositories/     # BookingRepository / IBookingRepository (in-memory store)
│   ├── Models/           # Booking, CreateBookingRequest, Result
│   └── Program.cs        # App startup + dependency injection
└── Tests/                # xUnit tests (BookingServiceTests)
```

Dependency injection is wired up in `Program.cs`: the repository is registered as a singleton (it holds the in-memory data), and the service as scoped.

## Getting started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Run the API

```bash
dotnet run --project API
```

The API will start on the URL shown in the console output (e.g. `http://localhost:5xxx`). Endpoints are served under `/api/bookings`.

### Run the tests

```bash
dotnet test Tests
```

## API reference

Base route: `/api/bookings`

| Method   | Route                | Description                  | Success response          |
| -------- | -------------------- | ---------------------------- | ------------------------- |
| `GET`    | `/api/bookings`      | List all bookings            | `200 OK` + bookings       |
| `GET`    | `/api/bookings/{id}` | Get a booking by id          | `200 OK` + booking        |
| `POST`   | `/api/bookings`      | Create a booking             | `201 Created` + booking   |
| `DELETE` | `/api/bookings/{id}` | Cancel (delete) a booking    | `204 No Content`          |

### Create booking request body

```json
{
  "roomId": 1,
  "from": "2025-01-01T09:00:00",
  "to": "2025-01-01T12:00:00"
}
```

### Error responses

Failures return a `ProblemDetails` payload with the matching status code:

| Outcome           | Status code         |
| ----------------- | ------------------- |
| `NotFound`        | `404 Not Found`     |
| `ValidationError` | `400 Bad Request`   |
| `Unauthorized`    | `401 Unauthorized`  |
| `Conflict`        | `409 Conflict`      |

For example, creating a booking that overlaps an existing one for the same room returns `409 Conflict`.

## Notes

Bookings are stored in memory and are **not** persisted — restarting the application clears all data.
