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
- Styled Components (CSS)

## The application

- Web API (Backend): A Web API that provides RESTful endpoints for task management of hardcoded cities.
- React Client (Frontend): A single-page client created with React.js, enabling users to see the weather of Stockholm, as well as reading and saving favorite cities.

## CI/CD Pipeline

I've implemented Azure Pipelines for continuous integration, automating the testing process and deployment, with jobs running on a self-hosted agent pool.
The pipelines was defined using the visual designer, which made it much easier for me as I am new to azure DevOps. With my source code hosted on GitHub, i am able to trigger my pipeline and initiate a job when a commit is made on main. Finally, the release pipeline is triggered by the new artifact, and deploys the application to an azure web app service.
the following steps define my pipeline:

- Restore
- Test
- Build
- Publish

Release pipeline:
- Deploy Azure App Service


## The TDD workflow
Throughout the development process, I have followed a TDD approach, trying to ensure code quality, reliability, and maintainability. Within the same solution, there is the main project and the test project. Before adding new functionality in the main project, I started writing a unit test that described a specific behaviour, then I implemented the code required to make the test pass. If a test failed, i continued to fix the code so that all test passed. 
This project doesn't contain a lot of code, and the tests don't cover all scenarios, but I definitely got a feel and a deeper understanding of the benefits with test-driven development while working on this project.

## Demo
<a>https://youtu.be/4HKK_nTL3Tk</a>

