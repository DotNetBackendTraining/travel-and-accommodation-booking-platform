# Travel and Accommodation Booking Platform

[![.NET and Docker](https://github.com/DotNetBackendTraining/travel-and-accommodation-booking-platform/actions/workflows/build-test-push.yml/badge.svg)](https://github.com/DotNetBackendTraining/travel-and-accommodation-booking-platform/actions/workflows/build-test-push.yml)

## Project Graphs

### Architecture Component Diagram

![Excalidraw-ComponentDiagram](assets/Excalidraw-ComponentDiagram.png)

https://excalidraw.com/#json=gnhjXbXssmpgWOvIyoS-H,R0_YDxdKLpHafGSdF7pmAQ

### User Experience

[Excalidraw — Collaborative whiteboarding made easy](https://excalidraw.com/#json=E95OwtS_yCQIeY6z2C1y2,hVOE2l7QvJXilwsDqrIqDg)

https://excalidraw.com/#json=E95OwtS_yCQIeY6z2C1y2,hVOE2l7QvJXilwsDqrIqDg

## 1. Login Page

- Fields for entering a username and password.

## 2. Home Page

### **2.1 Robust Search Functionality**

- Central search bar with the placeholder: "Search for hotels, cities..."
- Interactive calendar for selecting check-in and check-out dates, auto-set to today and tomorrow.
- Adjustable controls for specifying the number of adults (default: 2) and children (default: 0).
- Room selection option with a default setting of one room.

### 2.2 Featured Deals Section

- Renamed as "Featured Deals" to highlight special offers.
- Display of 3-5 hotels, each with a thumbnail, hotel name, location, and both original and discounted prices.
- Inclusion of star ratings for each featured hotel.

### 2.3 User's Recently Visited Hotels

- A personalized display of the last 3-5 hotels the user visited.
- Details like thumbnail image, hotel name, city, star rating, and pricing information are included.

### 2.4 Trending Destination Highlights

- A curated list of popular cities, each with a visually appealing thumbnail and city name.
- We need TOP 5 Cities has been visited the most in the system.

## **3. Search Results Page**

### 3.1 Comprehensive Search Filters

- Sidebar with filters such as price range, star rating, and amenities.
- Filter for different types of rooms like luxury, budget, or boutique hotels.

### 3.2 Hotel Listings

- List of hotels matching the search criteria with an infinite scroll feature.
- Each hotel's entry to include a thumbnail, name, star rating, price per night, and a brief description.

## **4. Hotel Page**

### 4.1 Visual Gallery

- High-quality images of the hotel, viewable in fullscreen mode.

### 4.2 Detailed Hotel Information

- Hotel name, star rating, description or history, and guest reviews.
- An interactive map showing the hotel's location with nearby attractions.

### 4.3 Room Availability and Selection

- List of different room types with images, descriptions, and prices.
- "Add to cart" option for easy booking.

## **5. Secure Checkout and Confirmation**

### 5.1 User Information and Payment

- Form for personal details and payment method.
- Fields for special requests or remarks.
- [`Optional`] check any payment third party and integrate your app with it

### 5.2 Confirmation Page

- Shows booking details like confirmation number, hotel address, room details, dates, and total price.
- Options to print or save the confirmation as a PDF.
- Send an email to the user email with the payment status and the invoice details.

## **6. Admin Page for Easy Management**

### 6.1 Functional Left Navigation

- Collapsible navigator with links to Cities, Hotels, and Rooms.

### 6.2 Admin Search Bar

- Filters for the grids.

### 6.3 Detailed Grids

- Cities: Name, Country, Post Office, Number of hotels, creation and modification dates, and delete option.
- Hotels: Name, star rate, owner, room number, creation and modification dates, delete option.
- Rooms: Number, availability, adult and children capacity, creation and modification dates, delete option.

### 6.4 Create Button

- Opens a form for creating Cities, Hotels, or Rooms.

### 6.5 Entity Update Form

- Accessible by clicking on a grid row.
- Forms for updating City (Name, Country, Post Office), Hotel (Name, City, Owner, Location), and Room (Number, Adults,
  Children).`x

## **Technical Requirements and Project Management:**

### **Technical Requirements**

1. **Design and Implementation of APIs**
    - Focus on creating APIs that are well-designed, maintainable, and efficient.
    - Use RESTful principles for API design, ensuring clear, well-documented endpoints.
2. **Clean Code Principles**
    - Write code that is readable, maintainable, and adheres to standard coding conventions.
    - Implement consistent naming conventions, code structuring, and comments for better readability.
3. **Efficient Handling of Data and Resources**
    - Optimize data storage, retrieval, and manipulation for performance and scalability.
    - Ensure efficient use of server resources, avoiding memory leaks and unnecessary processing.
4. **Robust Error Handling and Logging**
    - Implement comprehensive error handling to catch and manage exceptions gracefully.
    - Use logging to track errors, user actions, and system behavior for easier debugging and monitoring.
5. **Secure JWT Authentication**
    - Implement JSON Web Token (JWT) authentication to securely handle user sessions.
    - Ensure tokens are stored and transmitted securely.
6. **User Permissions Management**
    - Design a robust permission system to control user access to different parts of the application.
    - Implement role-based access control (RBAC) for fine-grained permissions.
7. **Unit Testing**
    - Develop and maintain a suite of unit tests to ensure the functionality, reliability, and ( bonus ) performance of
      the App.
    - Ensure that tests cover a wide range of scenarios, including edge cases, to validate the robustness of the API.
8. Integration and API testing

### **Additional Technical Requirements**

1. **Documentation**
    - Maintain up-to-date and detailed documentation for the codebase and APIs.
    - Include setup guides, API usage examples, and environment configuration instructions.
2. **[ `Bonus`] -** **Deploying the Project**
    - Set up a CI/CD pipeline for automated testing and deployment.
    - Ensure the application is deployable on cloud platforms like AWS, Azure, or GCP.
    - Include containerization (e.g., Docker) for consistent environments across development and production.

### **Project Management**

1. **Use of Jira (or other Project Management apps) for Task Management**
    - Utilize Jira effectively for tracking progress, managing tasks, and organizing sprints.
    - Create clear, actionable user stories and tasks, with well-defined acceptance criteria.
2. **Progress Tracking and Reporting**
    - Regularly update tasks with current status and any blockers.