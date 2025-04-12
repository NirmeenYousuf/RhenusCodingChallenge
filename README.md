# Rhenus Coding Challenge

This ASP.NET REST API is created as part of the Rhenus coding challenge. It is a **Game of Chance** API that follows the rules below:

## Game Rules
- A random number between **0** and **9** is generated.
- The player predicts the random number.
- The player starts with **10,000 points** and can wager them on their prediction, which they will either win or lose.
- Any number of points can be wagered.
- If the player is correct, they win **9 times their stake**.
- If the player is incorrect, they lose their stake.

## Solution Structure

The main solution consists of two projects:
1. **API**: Contains the core functionality of the game, including the game logic, player management, and betting system.
2. **Tests**: This project contains unit tests for services and integration tests for the API endpoints.

### Folder Structure
The API is divided into a modular structure with different directories to promote scalability and maintainability. The directories include:
- **Controllers**: Contains API controllers that handle incoming HTTP requests.
- **Services**: Contains the business logic, such as handling bets, managing player accounts, and generating random numbers.
- **Repositories**: Handles data access and persistence (in this case, a simple in-memory store).

### Game State
The game state is maintained so that bets can be placed multiple times with the same random number. Player data regarding the current account balance is stored in an in-memory dictionary object.

### Game Controller
The **GameController** exposes two primary API routes:
1. **Bet**: Allows a player to place a bet by providing the bet amount and their predicted number.
2. **Reset**: Resets the game by generating a new random number and resetting all players' accounts to their default value (10,000).

### Player Controller
The **PlayerController** exposes three API routes:
1. **Create Player**: Creates a player with a given ID and assigns a default account value of 10,000 points if no player with the same ID exists.
2. **Get Player**: Returns the current account balance of a player given their player ID.
3. **Reset Player**: Resets the player's account balance to the default value of 10,000.

## Features and Future Improvements

### **Scalability and Architecture Improvements**
In the future, as the project expands, it can be split into multiple layers such as:
- **Database Layer**: To persist player data and game state. Instead of using an in-memory dictionary, a relational or NoSQL database can be used for data storage.
- **Service Layer**: Where business logic will be separated into dedicated services.
- **Asynchronous Operations**: Asynchronous programming can be introduced for service and repository requests (using `async`/`await`) to improve scalability and avoid blocking threads.
- **Queues**: For handling long-running operations or scaling the game logic by decoupling requests with message queues (like RabbitMQ or Kafka) for improved concurrency and asynchronous operations.

### **Test Coverage**
- **Unit Tests**: Currently, the project has a comprehensive suite of unit tests for services to validate core business logic.
- **Integration Tests**: The API routes are covered by integration tests to ensure the system works as expected end-to-end.
- **100% Test Coverage**: Future work will focus on improving test coverage to 100% to ensure robust functionality and minimize bugs in production.

### **CI/CD and Containerization**
- **Dockerization**: The API can be containerized using Docker for easier deployment and scaling. Docker images will be used to ensure consistency between environments (local, staging, production).
- **CI/CD Pipeline**: A Continuous Integration (CI) and Continuous Deployment (CD) pipeline will be set up to automate the build, test, and deployment process. The pipeline will ensure that:
    - Code is automatically built and tested.
    - New code changes are validated through unit and integration tests.
    - Any new code pushed to the repository will be automatically deployed to production after successful tests.

### **Security Considerations**
- **Authentication**: In a real-world scenario, player authentication could be introduced using JWT (JSON Web Tokens) to ensure that only authorized users can access certain routes.
- **Authorization**: Roles can be defined for different users (e.g., admin, regular player) to restrict access to certain routes, like the **Reset** route for the game or individual players.

### **Future Enhancements**
- **Scaling**: As the system grows, we can consider adding more complex features such as multiple games, leaderboards, and social features.
- **Error Handling**: More robust error handling and exception management can be implemented to provide a better experience to users.

---

## Installation and Setup

### Prerequisites:
1. .NET SDK (version 8 or above) is required to build and run the application.
2. Visual Studio Code or JetBrains Rider (IDE) is recommended for development.

### Steps to Run Locally:
1. Clone the repository:
    ```bash
    git clone https://github.com/NirmeenYousuf/RhenusCodingChallenge.git
    ```
2. Navigate to the solution folder:
    ```bash
    cd RhenusCodingChallenge
    ```
3. Restore the NuGet packages:
    ```bash
    dotnet restore
    ```
4. Build the solution:
    ```bash
    dotnet build
    ```
5. Navigate to API folder and run the API:
    ```bash
    dotnet run
    ```
   The API should now be available at `http://localhost:5206`.

6. You can test the API using tools like **Postman** or **Swagger UI**. Swagger documentation is automatically available at `http://localhost:5206/swagger`.

### Testing:
To run the tests, use the following command:

```bash
dotnet test
