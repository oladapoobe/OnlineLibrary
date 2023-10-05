# OnlineLibrary
# Web-API

 OnlineLibrary Web API is a solution built with ASP.NET Core following Domain-Driven Design (DDD) principles. This solution provides a robust and maintainable structure that facilitates scalable and secure features for managing Users and Books.

## Table of Contents
- [Getting Started](#getting-started)
- [Usage](#usage)
- [Unit Testing](#unit-testing)



## Getting Started

### Prerequisites
- .NET 6 or later
- Visual Studio 2023 or Visual Studio Code with C# extension

### Installation
1. Clone the repository:
   ```sh
   [git clone https://github.com/oladapoobe/OnlineLibrary.git ]
Usage
This API exposes several endpoints for managing Users and Books. To interact with these endpoints, send HTTP requests with the required headers and payload.

Headers
All AddUser, AddBook, and UpdateBook endpoints require the following headers:

DateValue: Represents the date and time the request is sent. Use the format yyyy-MM-ddTHH:mm:ss.
CreatedBy: Represents the username or identifier of the person initiating the request.

Endpoints
Add User
URL: http://localhost:5254/Auth/AddUser
Method: POST
Payload Example:
json
{
  "username": "JohnDoe",
  "password": "securepassword"
}
Add Book
URL: http://localhost:5254/Books/AddBook
Method: POST
Payload Example:
json

{
  "name": "Sample Book",
  "author": "John Doe",
  "publisher": "XYZ Publications"
}
Update Book
URL: http://localhost:5254/Books/UpdateBook
Method: PUT
Payload Example:
json

{
  "id": "1",
  "name": "Updated Sample Book",
  "author": "John Doe",
  "publisher": "XYZ Publications"
}

e.t.c
Unit Testing
This project includes unit tests written using the NUnit testing framework
