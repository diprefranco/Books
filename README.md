Books API
============
This project is an ASP.NET Core Web API that returns fake books and authors. It was created to learn and practice asynchronous programming in ASP.NET Core Web API development.

## Features
- Fetch a list of books.
- Stream books asynchronously using Server-Sent Events (SSE).
- Fetch a single book with its fake covers.
- Add a new book using POST requests.
- Implements filters, AutoMapper, and dependency injection.

## Main endpoints
- GET `/api/books`: Retrieves all books.
- GET `/api/bookstream`: Streams books asynchronously.
- GET `/api/books/{id}`: Retrieves a specific book by its ID.
- POST `/api/books`: Creates a new book.

## Example Usage
Use an API client like [Postman](https://www.postman.com/) to interact with the endpoints.

## Version Information
This project was built with the .Net 9.0 framework.

## Copyright
This API is for learning purposes and was built from the course ["Developing an Asynchronous ASP.NET Core Web API"](https://app.pluralsight.com/library/courses/asp-dot-net-core-6-web-api-developing-asynchronous/table-of-contents) by Kevin Dockx.

## Author
GitHub: [Franco Dipre](https://github.com/diprefranco/)

## License
[MIT LICENSE](LICENSE)
