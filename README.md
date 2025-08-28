# Country Explorer App


A full-stack solution for the "Country Explorer" challenge using **.NET 9** (Clean Architecture) and **Angular 20**.


## Features
- Backend REST API (OpenAPI spec provided in challenge) with `/countries` and `/countries/{name}`.
- Clean Architecture (Domain, Application, Infrastructure, WebApi).
- Frontend Angular app with:
- Home grid of countries list with their respective flags
- Country detail page (name, population, capital, region)
- Frontend fetches **flag images from REST Countries** API, while **data** comes from backend.
- Unit & integration tests for both tiers.
- GitHub Actions CI to build & test both.


## How to Run


### Prereqs
- .NET SDK 9
- Node.js 20+
- Angular 20
- PNPM or NPM (project uses npm scripts)


### Backendcd backend/src/CountryExplorer.WebApi
dotnet run
# Swagger: https://localhost:7114
# Angular: http://localhost:4200
- API documentation: [https://localhost:7114]

### Running the Frontend
- App URL: [http://localhost:4200]

## Testing

- **Backend:** Run tests with `dotnet test` in the backend solution directory.
- **Frontend:** Run tests with `npm test` in the frontend directory.

## Project Structure