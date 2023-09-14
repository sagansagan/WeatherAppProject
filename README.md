# WeatherAppProject

Welcome to the Weather App Project! This project showcases the creation of a solution that leverages a simple Web API for backend functionality, a React Client for the user interface, Test-Driven Development (TDD) for code quality, and Azure Pipelines for continuous integration and deployment.
The main focus of this project is partly to work test-driven and partly to learn what a CI/CD pipeline is and how it is used.

<h2>Tech stack</h2>
<h4>Backend</h4>

- C#
- .NET
- xUnit
- Framework: .NET 6.0.
- ASP.NET core Minimal API.

<h4>Frontend</h4>

- Javascript (React.js)

## The application

- Web API (Backend): A Web API that provides RESTful endpoints for task management of hardcoded cities.
- React Client (Frontend): A single-page client created with React.js, enabling users to see the weather of Stockholm, as well as reading and saving favorite cities.

## CI/CD Pipeline

I've implemented Azure Pipelines for continuous integration, automating the testing process and deployment, with jobs running on a self-hosted agent pool.
The pipeline was defined using the visual designer, which made it much easier for me as I am new to azure devops. With my source code hosted on GitHub, i am able to trigger my pipeline and initiate a job when a commit is made on main.
the following steps define my pipeline:

- Restore
- Test
- Build
- Publish

## Workflow


