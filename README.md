# Hotel Booking System - Backend

A hotel booking management system built with ASP.NET Core, following Clean Architecture principles with CQRS pattern.

## Architecture

The project follows Clean Architecture with the following layers:

- **HotelBooking.Web** - ASP.NET Core Web API (Controllers, DTOs, Validators)
- **HotelBooking.BLL** - Business Logic Layer (Services)
- **HotelBooking.DAL** - Data Access Layer (Commands, Queries, Handlers)
- **HotelBooking.Database** - Database Layer (Entities, DbContext, Migrations)
- **HotelBooking.Shared** - Shared Models and Utilities

## Technology Stack

- **.NET 10.0** - Framework
- **ASP.NET Core** - Web API
- **Entity Framework Core** - ORM
- **Pomelo.EntityFrameworkCore.MySql** - MySQL Provider
- **Dapper** - Raw SQL queries for analytics
- **MediatR** - CQRS pattern implementation
- **FluentValidation** - Request validation
- **JWT Bearer Authentication** - Authentication & Authorization
- **ASP.NET Core Identity** - User management

## Features

### User Features
- User registration and authentication
- Browse hotels and rooms with filters (city, dates, price, place amount)
- Create, view, and cancel bookings
- Time-based bookings (hourly precision)
- Booking conflict detection with 1-hour buffer

### Admin Features
- Hotel and room management (CRUD operations)
- Booking management (view, edit, delete)
- Analytics dashboard:
  - Monthly booking statistics
  - Monthly income statistics (USD per hour)

### Technical Features
- Soft delete for hotels and rooms
- Pagination for list endpoints
- Advanced filtering (city, street, dates, price range, place amount)
- CORS support
- Docker containerization

## Getting Started

### Prerequisites

- .NET 10.0 SDK
- MySQL 8.0+
- Docker and Docker Compose (optional)

### Local Development

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd HotelBooking
   ```

2. **Configure database connection**
   
   Update `HotelBooking.Web/appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Port=3306;Database=hotelbooking;User=root;Password=yourpassword;AllowUserVariables=True;UseAffectedRows=False;"
     }
   }
   ```

3. **Run the application**
   ```bash
   dotnet run
   ```

   The API will be available at `http://localhost:5203`

### Docker Setup

1. **Create `.env` file** (copy from `.env.example`):
   ```env
   MYSQL_ROOT_PASSWORD=rootpassword
   MYSQL_DATABASE=hotelbooking
   MYSQL_USER=hoteluser
   MYSQL_PASSWORD=hotelpassword
   JWT_SECRET=Super-Secret-Key-That-Is-At-Least-32-Characters-Long-123456!
   ADMIN_EMAIL=admin@example.com
   ADMIN_PASSWORD=password123!
   ADMIN_FIRST_NAME=Admin
   ADMIN_LAST_NAME=User
   FRONTEND_URL=http://localhost:3000
   BACKEND_URL=http://localhost:5203
   ```

2. **Start services**
   ```bash
   docker-compose up -d
   ```

   Backend will be available at `http://localhost:5203`

## API Endpoints

### Authentication
- `POST /api/login` - User login
- `POST /api/register` - User registration

### User Endpoints

#### Hotels
- `GET /api/user/hotels` - Get hotels list (with filters)
- `GET /api/user/hotels/{id}` - Get hotel details

#### Rooms
- `GET /api/user/rooms` - Get rooms list (with filters)
- `GET /api/user/rooms/{id}` - Get room details
- `GET /api/user/rooms/hotel/{hotelId}` - Get rooms by hotel

#### Bookings
- `GET /api/user/bookings` - Get user bookings
- `POST /api/user/bookings` - Create booking
- `PUT /api/user/bookings/{id}` - Update booking
- `DELETE /api/user/bookings/{id}` - Cancel booking

### Admin Endpoints

#### Hotels
- `GET /api/admin/hotels` - Get all hotels
- `POST /api/admin/hotels` - Create hotel
- `PUT /api/admin/hotels/{id}` - Update hotel
- `DELETE /api/admin/hotels/{id}` - Delete hotel (soft delete)

#### Rooms
- `GET /api/admin/rooms` - Get all rooms
- `GET /api/admin/rooms/hotel/{hotelId}` - Get rooms by hotel
- `GET /api/admin/rooms/{id}` - Get room details
- `POST /api/admin/rooms` - Create room
- `PUT /api/admin/rooms/{id}` - Update room
- `DELETE /api/admin/rooms/{id}` - Delete room (soft delete)

#### Bookings
- `GET /api/admin/bookings` - Get all bookings
- `GET /api/admin/bookings/hotel/{hotelId}` - Get bookings by hotel
- `GET /api/admin/bookings/room/{roomId}` - Get bookings by room
- `PUT /api/admin/bookings/{id}` - Update booking
- `DELETE /api/admin/bookings/{id}` - Delete booking

#### Analytics
- `GET /api/admin/analytics/bookings` - Get booking statistics (monthly)
- `GET /api/admin/analytics/incomes` - Get income statistics (monthly, USD/hour)

### Selectors
- `GET /api/selectors/cities` - Get all cities (with search)
- `GET /api/selectors/cities/used` - Get cities with hotels (with search)

## Authentication

The API uses JWT Bearer authentication. Include the token in the Authorization header:

```
Authorization: Bearer <your-token>
```

### Roles
- **User** - Regular user (can manage own bookings)
- **Administrator** - Admin user (full access)
