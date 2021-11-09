# CryptoInfo

Goal of the project is to build a system where one can import transactions and programmatically process them. One possible use-case would be calculating the gains / losses for tax reports.

Every component in the project is work in progress. The program is currently filled with bugs and is published to showcase my current coding style, not to be used. **Note: current web UI is purely for trying out that the API calls work.**

## Requirements
- .NET 5
- Node / NPM for frontend React app

## Project structure

Idea behind the project structure is dividing the project into domain, application, infrastructure and representation layers.
- Domain
  - all the base classes needed for the project, which could be easily integrated to other projects
- Application
  - includes service descriptions (interfaces) and application logic for how to use the services
  - any representation application can use the same logic without changes (be it web API, console application etc.)
- Infrastructure
  - has all service implementations
  - interfaces may be implemented in many ways (like saving transactions to DB or in-memory)
  - provides a plug-and-play services - developer can select which implementations they want to inject
- representation (currently CryptoInfo)
  - uses previous projects to create a system which users can interact with the actual application
  - CryptoInfo is an web application with React frontend
- Tests
  - all tests will be under this project