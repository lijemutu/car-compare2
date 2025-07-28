# MVP Tasks: New Cars Mexico 2025 (.NET 9 + HTMX)

## Data Layer
- [X ] Design C# models to represent car data (based on cars2025.json structure).
- [X ] Implement a service to load and parse cars2025.json.
- [X ] Add a repository/service for querying/filtering car data.

## API Layer
- [X ] Create minimal API endpoints to serve car data (list, details, filter).
- [X ] Ensure endpoints return JSON and/or HTML fragments for HTMX.

## UI Layer (HTMX)
- [X ] Set up basic HTML layout with .NET 9 Razor Pages or minimal API endpoints.
- [X ] Display a list of all cars with key info (make, model, year, price).
- [X ] Implement car detail view (HTMX partials/fragments).
- [X ] Add filtering/search UI (by make, model, price, etc.) using HTMX requests.
- [X ] Add pagination or lazy loading for large lists.

## Other
- [X] Add basic error handling and validation.
- [X] Document API endpoints and data model.
- [X] Add README with setup instructions.

## Stretch Goals (Optional)
- [X] Add comparison feature for selected cars.
- [X] Add sorting options (price, year, fuel efficiency).
- [X] Add responsive/mobile-friendly UI.
