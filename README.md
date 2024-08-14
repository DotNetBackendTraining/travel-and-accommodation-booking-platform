# Travel and Accommodation Booking Platform

[![.NET and Docker](https://github.com/DotNetBackendTraining/travel-and-accommodation-booking-platform/actions/workflows/build-test-push.yml/badge.svg)](https://github.com/DotNetBackendTraining/travel-and-accommodation-booking-platform/actions/workflows/build-test-push.yml)

## Overview

This is an ASP.NET Core API project for an advanced online hotel booking system. Which includes the Login Page, Home
Page, Search Results, Hotel Details, Secure Checkout, and Admin Management.

## Table of Contents

1. [Overview](#overview)
2. [Project Requirements](#project-requirements)
3. [Domain Models](#domain-models)
4. [Component Diagram](#component-diagram)
5. [Getting Started](#getting-started)

## Project Requirements

To understand the project requirements in detail, please refer to
the [Project Requirements documentation](documentations/ProjectRequirements.md).

## Domain Models

The domain models used in this project were derived from the project requirements to reflect the business logic
accurately.
To view the detailed domain models, please refer to the [Domain Models documentation](documentations/DomainModels.md).

## Component Diagram

This project is designed using the **Clean Architecture**.
Here is an overview of the architecture of the project:

![Excalidraw-ComponentDiagram](documentations/Excalidraw-ComponentDiagram.png)

## Getting Started

### Setup

Before running the project, you need to configure the application settings.
Add the following `appsettings.json` in the `TravelAccommodationBookingPlatform.App` project:
(You can find these values in the Jira project site)

```json
{
  "CloudinarySettings": {
    "CloudName": "...",
    "ApiKey": "...",
    "ApiSecret": "..."
  }
}
```

### Running

The project uses docker-compose which includes a SQL Server image, so no need to install it locally.
Use the following command to run docker-compose:

```bash
docker-compose up
```