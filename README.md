# MediCare Hospital Appointment Management

MediCare is an ASP.NET Core MVC college practical demonstration. It provides a polished doctor directory, in-memory appointment request flow, asynchronous external API page, and a focused demonstration of cookie, session, query-string, and hidden-field state.

## Technologies

- ASP.NET Core MVC on `net10.0`
- C# with Razor views and dependency injection
- Bootstrap assets already included in the project
- `IMemoryCache`, session with distributed memory cache, response caching, and `HttpClientFactory`
- No database or additional package dependency

## Run

```text
dotnet restore
dotnet build
dotnet run --launch-profile http
```

Open `http://localhost:5086`.

## Structure and requirements

- `Models/Doctor.cs` and `Extensions/DoctorExtensions.cs`: validated doctor data and the shared `CalculateTotalCharge()` extension. Every total adds exactly `₹100`.
- `Services/DoctorService.cs`: owns six sample doctors and caches the directory under a stable key for exactly three minutes.
- `Controllers/PostsController.cs`: uses an injected named `HttpClient`, `async`/`await`, safe JSON deserialization, cancellation, timeout, network, and HTTP error handling.
- `Controllers/StateManagementController.cs`: stores a non-sensitive patient name in a short-lived cookie and a specialization in server-side session. The page also links to `/Doctors/Details?id=1` and explains the appointment hidden field.
- `DoctorsController.Details`: uses query-string `id` validation and `[ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]`. The endpoint contains only public doctor information and therefore can be cached.
- `AppointmentsController`: validates input, verifies the doctor from the service, recalculates pricing server-side, and uses anti-forgery validation. The form carries the selected doctor in a hidden `DoctorId` field; the visible selection is only a user-friendly controller for that value.

## Demonstration routes

- `/` - dashboard and capabilities
- `/Doctors` - cached in-memory directory
- `/Doctors/Details?id=1` - query-string details page and response-cache headers
- `/Appointments/Create?doctorId=1` - preselected appointment form
- `/Posts` - asynchronous JSONPlaceholder posts table
- `/StateManagement` - cookie, session, query string, and hidden-field lab

## Practical checklist

1. Open the dashboard and explain the service fee and in-memory data.
2. Open the directory, inspect a details URL, and check its `Cache-Control` response header.
3. Book with a doctor, try invalid or past input, then submit a valid request.
4. Inspect the appointment form source for the hidden `DoctorId` and explain server-side revalidation.
5. Save and clear the cookie and change the session specialization on the state page.
6. Open External Posts and explain the injected asynchronous API request and graceful error state.

## Limitations

Appointment confirmations are held only for the current request and are not persisted. Doctor data is sample data cached in process for three minutes. Response caching is intended only for the public doctor details endpoint; patient-specific and state-changing actions are not marked for public caching. The external posts page depends on JSONPlaceholder being reachable.
