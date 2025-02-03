# Best Stories API

# Overview

This project is a RESTful API built using ASP.NET Core 8 to retrieve the top n best stories from the Hacker News API based on their score. The n value is specified by the caller. The project is developed based on the documentation of the Hacker News API: https://github.com/HackerNews/API

# Features

Fetches and returns the top n best stories in descending order of score.

Caches results to reduce load on the Hacker News API.

Implements dependency injection for better maintainability.

Implements logging for tracking state at runtime.

Includes unit tests using xUnit and Moq.

# Prerequisites

.NET 8 SDK

Visual Studio 2022 (or any preferred IDE)

Git Bash

Docker (optional for containerization)

# Running the Application

Clone the Repository

git clone https://github.com/kamalcodeag/BestStories.git

Install Dependencies

dotnet restore

Run the API

via command line: dotnet run

or

via IIS: Click Run or Ctrl + F5 setting the app to IIS Express in Visual Studio 2022.

or

via Docker (Windows): Run Docker Service and click Run or Ctrl + F5 setting the app to Container (Dockerfile) in Visual Studio 2022.

# Test the API

Open its Swagger url in a web browser to see the endpoint: https://localhost:44386/swagger/index.html

# Assumptions

Best Stories API is highly available. In case the API is down, an error response is returned.

Best Stories API is unresponsive when Hacker News API is down.

Caching is required to prevent rate limiting issues.

Stories without a score are ignored.

# Enhancements (In case of given more time)

Add Pagination to fetch stories incrementally.

Implement Distributed Caching (e.g., Redis) for improved efficiency.

Add Rate Limiting to prevent API abuse.

Implement Logging & Monitoring using Serilog or Application Insights.

# Running Tests

via command line: dotnet test

or

via Visual Studio 2022: Click "Run All Tests" through "Test" section in the IDE toolbar (Ctrl + R, A).
