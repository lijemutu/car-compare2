# API Documentation

This document provides details about the API endpoints and data models used in the Car Compare application.

## Endpoints

### GET /api/cars

Returns a list of all cars.

**Query Parameters:**

- `make` (string): Filter by car make.
- `model` (string): Filter by car model.
- `year` (int): Filter by car year.
- `minPrice` (decimal): Filter by minimum price.
- `maxPrice` (decimal): Filter by maximum price.

**Example:**

```
GET /api/cars?make=Toyota&year=2023
```

### GET /api/cars/{id}

Returns the details of a specific car.

**Example:**

```
GET /api/cars/123
```

## Data Model

### Car

| Field               | Type     | Description                               |
| ------------------- | -------- | ----------------------------------------- |
| `id`                | int      | The unique identifier for the car.        |
| `make`              | string   | The manufacturer of the car.              |
| `model`             | string   | The model of the car.                     |
| `year`              | int      | The manufacturing year of the car.        |
| `price`             | decimal  | The price of the car in USD.              |
| `engine`            | string   | The engine specifications of the car.     |
| `horsepower`        | int      | The horsepower of the car's engine.       |
| `fuel_efficiency`   | string   | The fuel efficiency of the car.           |
| `safety_features`   | string[] | A list of safety features available in the car. |
