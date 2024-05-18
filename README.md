# Wallets Service
We have a service to manage our wallets. Our players can top-up their wallets using a credit card and spend that money on our platform (bookings, racket rentals, ...)

That service has the following operations:
- You can query your balance.
- You can top-up your wallet. In this case, we charge the amount using a third-party payments platform (stripe, paypal, redsys).
- You can spend your balance on purchases. (TODO)
- You can return these purchases, and your money is refunded. (TODO)
- You can check your history of transactions.

This exercise consists of building a proof of concept of that wallet service.
You have to code endpoints for these operations:
1. Get a wallet using its identifier.
1. Top-up money in that wallet using a credit card number. It has to charge that amount internally using a third-party platform.



The basic structure of a wallet is its identifier and its current balance. 

You can also find an implementation of the service that would call to the real payments platform (StripePaymentService).
This implementation is calling to a simulator deployed. Take into account
that this simulator will return 422 http error codes under certain conditions.

Consider that this service must work in a microservices environment in high availability. You should care about concurrency too.


# 🏗️ Clean Architecture

Welcome to the Clean Architecture project! This repository demonstrates the implementation of Clean Architecture principles in a .NET application. The architecture is organized into distinct layers, ensuring separation of concerns, scalability, and ease of maintenance. Below is an overview of each layer and its components.

---

## 📂 Layers Overview

### Presentation Layer (User Interface)

This layer handles all user interactions and displays the user interface.

- **Controllers**
  - `UserController.cs`
  - `ProductController.cs`
- **Views**
  - `UserView.cs`
  - `ProductView.cs`
- **Middleware**
  - `ErrorHandlingMiddleware.cs`
  - `LoggingMiddleware.cs`
- **Mapping**
  - `AutoMapperConfig.cs`
    - If you use a mapper in the application layer, create interfaces for the DTOs to decouple application and presentation layers.

-------------------------⬇️-------------------------

### Interface Adapters (Infrastructure)

This layer communicates with the external world, such as databases and external services.

- **Database**
  - `DbContext`
- **Repositories**
  - `UserRepository.cs`
  - `ProductRepository.cs`
- **Infrastructure Services**
  - `ExternalAPIService.cs`
  - `EmailService.cs`
  - `FileStorageService.cs`
- **Background Jobs**
  - `EmailJob.cs`
  - `FileCleanupJob.cs`

-------------------------⬇️-------------------------

### Application Layer (Application Business Rules)

This layer contains business logic and rules that are specific to the application's use cases.

- **Services**
  - `ProductService.cs`
  - `UserService.cs`
  - `IUserService.cs`
  - `IProductService.cs`
- **Use Cases**
  - `CreateUserUseCase.cs`
  - `WalletUseCase.cs`
- **Validators**
  - `CreateUserValidator.cs`
  - `WalletValidator.cs`
- **Queries**
  - `GetAllProductsQuery.cs`
  - `GetUserQuery.cs`
- **Commands**
  - `CreateUserCommand.cs`
  - `UpdateProductCommand.cs`

-------------------------⬇️-------------------------


### Domain Layer (Enterprise Business Rules)

This layer contains enterprise-wide business rules and logic.

- **Entities**
  - `User.cs`
  - `Product.cs`
- **Events**
  - `UserCreatedEvent.cs`
  - `ProductCreatedEvent.cs`
- **Repository Interfaces**
  - `IUserRepository.cs`
  - `IProductRepository.cs`
- **Exceptions**
  - `DomainException.cs`

---

## 🚀 Getting Started

To get started with the project, clone the repository and follow the instructions below.

### Prerequisites

- .NET SDK
- Entity Framework Core 8

