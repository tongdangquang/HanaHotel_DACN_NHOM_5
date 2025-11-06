# OtelAPI Project

Welcome to the **OtelAPI** project! This API is designed to manage hotel operations. Follow the instructions below to set up and run the project on your local machine.

## Prerequisites

Before starting, ensure you have the following installed:

- **MySQL Server**: The application uses MySQL as its database. You need a MySQL server running locally or on a remote machine.
- **.NET 8 SDK**: The project is built using **.NET 8**, so make sure you have the appropriate SDK installed. You can download it from the official [.NET website](https://dotnet.microsoft.com/en-us/download/dotnet/8.0).

For the best experience, we recommend using **dark mode**.

## Getting Started

To get started, follow these steps:

### 1. Clone the repository:
```bash
git clone https://github.com/hzuslu/HotelBookingAPI.git
cd otelapi
  ```

### 2. Configure the MySQL connection string:

Open the WebApi -> DataAccessLayer -> DataContext file and locate the connection string section. Update it to match your MySQL server credentials:

### 3. Apply migrations:

- **Add migrations using the following command**:
    ```
    add-migration YourMigrationsName
    ```
- **Create or update the database with the following command**:
    ```
    update-database
    ```

Admin Login
You can log in to the admin panel with the following credentials:

- **Username**: admin
- **Password**: Admin123.

## Contact Information
For inquiries, feedback, or contributions, feel free to reach out:

- **Email**: [hasanuslu0278@gmail.com](mailto:hasanuslu0278@gmail.com)

I welcome your input and am here to assist you with any questions or suggestions.
