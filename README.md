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


#Clean architecture
+-------------------------------------------+
|               Presentation                |
|           (User Interface)                |
|                                           |
|   +----------------------+                |
|   |      Controllers     |                |
|   |  - UserController.cs |                |
|   |  - ProductController.cs|              |
|   |                      |                |
|   +----------------------+                |
|   |         Views        |                |
|   |  - UserView.cs       |                |
|   |  - ProductView.cs    |                |
|   +----------------------+---------+      |
|   |      Middleware                |      |
|   |  - ErrorHandlingMiddleware.cs  |      |
|   |  - LoggingMiddleware.cs        |      |
|   +--------------------------------+      | 
|   |      Mapping                   |      |
|   |  - AutoMapperConfig.cs         |      | if you use mapper in the application layer
|   |                                |      | you need to create an interface for the DTOs
|   +--------------------------------+      | to decouple application and presentaton layers
+-------------------------------------------+ 
                           | 
                           v
+-----------------------------------------------------------+
|           Interface Adapters                              |
|            (Infrastructure)                               |
|                                                           |
|   +----------------------+   +-------------------------+  |
|   |     Database             |->|   Repositories          |
|   |   - DbContext            |  |  - UserRepository.cs    |
|   |                          |  |  - ProductRepository.cs |
|   +----------------------+   |  +-------------------+     |
|   |     Infrastructure       |                            |
|   | - ExternalAPIService.cs  |                            |
|   | - EmailService.cs        |                            |
|   | - FileStorageService.cs  |                            |
|   |  +-------------------+   |                            |
|   |  |  Background Jobs      |                            |
|   |  |  - EmailJob.cs        |                            |
|   |  |  - FileCleanupJob.cs  |                            |
|   |  +-------------------+   |                            |
|   +----------------------+--+-----------------------------+
|                                                           |
+-------------------------+---------------------------------+
                           | 
                           v
+---------------------------------------------+
|             Application                     |
|          (Application Business              |
|               Rules)                        |
|                                             |
|   +----------------------+                  |
|   |        Services      |                  |
|   |  - ProductService.cs |                  |
|   |  - UserService.cs    |                  |
|   |                      |                  |
|   |  - IUserService.cs   |                  |
|   |  - IProductService.cs|                  |
|   +------------------------+                |
|   |       Use Cases        |                |
|   |  - CreateUserUseCase.cs|                |
|   |  - WalletUseCase.cs    |                |
|   +---------------------------+             |
|   |       Validators          |             |
|   |  - CreateUserValidator.cs |             |
|   |  - WalletValidator.cs     |             |
|   +---------------------------+             |
|   |        Queries            |             |
|   |  - GetAllProductsQuery.cs |             |
|   |  - GetUserQuery.cs        |             |
|   +----------------------------+            |
|   |       Commands             |            |
|   |  - CreateUserCommand.cs    |            |
|   |  - UpdateProductCommand.cs |            |
|   +----------------------------+            |
+-------------------------+-------------------+
                           |
                           v
+---------------------------------------------+
|                   Domain                    |
|          (Enterprise Business               |
|                 Rules)                      |
|                                             |
|   +----------------------+                  |
|   |        Entities      |                  |
|   |  - User.cs           |                  |
|   |  - Product.cs        |                  |
|   |                      |                  |
|   +---------------------------+             |
|   |        Events             |             |
|   |  - UserCreatedEvent.cs    |             |
|   |  - ProductCreatedEvent.cs |             |
|   +---------------------------+             |
|   |   Repository Interfaces  |              |
|   |  - IUserRepository.cs    |              |
|   |  - IProductRepository.cs |              |
|   +----------------------+                  |
|   |      Exceptions      |                  |
|   |  - DomainException.cs|                  |
|   +----------------------+                  |
+--------------------------+------------------+ 