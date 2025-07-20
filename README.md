# Communication API

A comprehensive communication management system built with ASP.NET Core, featuring template management, customer management, and message sending capabilities with JWT authentication.

## Table of Contents

- [Features](#features)
- [Architecture](#architecture)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Configuration](#configuration)
- [Database Setup](#database-setup)
- [Running the Application](#running-the-application)
- [API Documentation](#api-documentation)
- [Authentication](#authentication)

## Features

- **Customer Management**: CRUD operations for customer data
- **Template Management**: Create and manage email templates with placeholders
- **Message Sending**: Send personalized messages using templates
- **JWT Authentication**: Secure API endpoints with token-based authentication

Extra features:
- **Swagger Documentation**: Interactive API documentation
- **Layered Architecture**: Clean separation of concerns (Domain, BLL, DAL, API)
- **Comprehensive Logging**: Structured logging throughout the application
- **Request Validation**: Middleware-based validation for all requests

## Architecture

The application follows a layered architecture pattern:

```
CommunicationAPI (Presentation Layer)
├── Controllers
├── Middleware
└── Model

BLL (Business Logic Layer)
├── TemplateService
└── CustomerService

DAL (Data Access Layer)
├── TemplateRepository
└── CustomerRepository

Domain (Core Layer)
├── Models
└── Interfaces
```

## Prerequisites

Before running this application, ensure you have the following installed:

- **.NET 8.0 SDK** or later
- **SQL Server** (LocalDB, Express, or Full Edition)
- **Visual Studio 2022** or **Visual Studio Code**
- **Git** (for cloning the repository)

## Installation
### Clone the Repository

URL: https://github.com/TadasLT/CommunicationApp

## Configuration
### 1. Database Connection String

Update the connection string in `CommunicationAPI/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CommunicationDB;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

### 2. JWT Configuration

Update JWT settings in `CommunicationAPI/appsettings.json`:

```json
{
  "Jwt": {
    "Key": "YourSuperSecretKeyHereMakeItLongEnoughForHS256Algorithm",
    "Issuer": "CommunicationAPI",
    "Audience": "CommunicationAPIUsers"
  }
}
```

## Database Setup

Use database backup from email: CommunicationDb.bak

## Running the Application
### Access Swagger Documentation

Open your browser and navigate to:
```
http://localhost:5222/swagger
```

## API Documentation
### Authentication

All API endpoints require JWT authentication. To get a token:

1. Use the `/api/Auth/Login` endpoint
2. Provide username="admin" and password="password" (admin:password)
3. Use the returned token in the Authorization header: `Bearer <token>`

### Available Endpoints

#### Customer Management
- `GET /api/Customer/GetAll` - Get all customers
- `GET /api/Customer/GetById/{id}` - Get customer by ID
- `POST /api/Customer/Create` - Create new customer
- `PUT /api/Customer/Update` - Update existing customer
- `DELETE /api/Customer/Delete/{id}` - Delete customer

#### Template Management
- `GET /api/Template/GetAll` - Get all templates
- `GET /api/Template/GetById/{id}` - Get template by ID
- `POST /api/Template/Create` - Create new template
- `PUT /api/Template/Update` - Update existing template
- `DELETE /api/Template/Delete/{id}` - Delete template

#### Communication
- `POST /api/Communication/SendMessage?customerId={id}&templateId={id}` - Send message using template

### Performance Optimization

1. **Database Indexing**: Add indexes on frequently queried columns
2. **Connection Pooling**: Configure connection pool size
3. **Caching**: Implement caching for frequently accessed data
4. **Logging Levels**: Adjust logging levels for production