# Car Compare App

This is a simple web application for comparing car prices, built with .NET 9 and HTMX.

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Node.js and npm](https://nodejs.org/) (for client-side dependencies)

### Installation

1. **Clone the repository:**
   ```bash
   git clone https://github.com/your-username/car-compare.git
   cd car-compare
   ```

2. **Restore .NET dependencies:**
   ```bash
   dotnet restore
   ```

3. **Install client-side dependencies:**
   ```bash
   npm install
   ```

4. **Run the application:**
   ```bash
   dotnet run --project CarCompare
   ```

The application will be available at `https://localhost:5001` (or `http://localhost:5000`).

## Usage

- Navigate to the home page to see a list of cars.
- Use the filter options to narrow down the results.
- Click on a car to view more details.
- Select cars to compare them side-by-side.

## Project Structure

- `CarCompare/`: Main project folder.
  - `Controllers/`: Contains API controllers.
  - `Models/`: Contains data models.
  - `Views/`: Contains Razor views and partials for HTMX.
  - `wwwroot/`: Contains static assets (CSS, JS, images).
  - `cars.json`: Data source for car information.
- `CarCompare.sln`: Solution file.
- `README.md`: This file.
