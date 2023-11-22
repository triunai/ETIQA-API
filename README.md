# 🌐 Complete Developer Network (CDN) - Freelancer Directory API

## 📖 Overview
This project is part of the Backend .NET Developer Assessment by ETIQA IT. It involves the creation of a RESTful API for the fictional company CDN (Complete Developer Network) to manage a directory of freelancers. The API provides functionality to register, update, delete, and retrieve user information, along with managing their skillsets.

## ✨ Features

### 👥 User Management
- **Create Users**: Register new users with details including username, email, phone number, skillsets, and hobbies.
- **Update Users**: Modify existing user details.
- **Delete Users**: Remove users from the system.
- **Get Users**: Retrieve a list of all registered users or specific user details.

### 🛠️ Skillset Management
- **Add Skillsets**: Create new skillsets that can be associated with users.
- **Update Skillsets**: Modify existing skillsets.
- **Delete Skillsets**: Remove skillsets from the system.
- **Get Skillsets**: Fetch all skillsets or a specific skillset's details.

## 🚀 Technologies Used
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server (RDBMS for data storage)

## 📆 Development Highlights

### 🗓️ Day 1
- Established foundational models for `User` and `Skillset`.
- Implemented CRUD operations for the `Skillset` entity.
- Configured many-to-many relationships between Users and Skillsets.
- Migrated and updated the database schema to reflect model changes.
- Ensured API endpoints for `Skillset` are fully functional.
- Set up a robust error handling and validation mechanism in API methods.

## 📚 Repository and Documentation
- Source code available in a GitHub repository (link to be provided).
- Detailed documentation on the API endpoints and their usage.

## 🌟 Future Enhancements
- Implement client-side development to interact with the API.
- Enhance endpoint security and implement caching strategies.
- Introduce pagination for large data sets.
- Develop a comprehensive testing strategy.
- Explore hosting options (e.g., Heroku, AWS) for a live demo.

## 📝 Instructions for Use
- Clone the repository.
- Configure the connection string to point to your SQL Server instance.
- Run database migrations to set up the schema.
- Start the application to interact with the API.

## 📢 Note to Reviewers
This README outlines the initial phase of the project. Subsequent enhancements and features will be documented in future updates.


1. User Management Module Development
Task: Implement CRUD operations for users.
Sub-tasks:
Create the User model with properties like username, email, phone number, skillsets, and hobbies.
Develop a repository with methods for adding, updating, deleting, and retrieving users.
Implement API endpoints in UserController for each operation.
Ensure proper validation and error handling in each endpoint.
Testing: Thoroughly test each endpoint, including the integration with skillsets.
2. Enhance Skillset Management Features
Task: Refine the skillset management functionality.
Sub-tasks:
Review and optimize existing CRUD operations for skillsets.
Enhance validation and error handling in SkillsetController.
Testing: Re-test all skillset endpoints to ensure robustness.
3. Integrate and Test Many-to-Many Relationship
Task: Ensure the many-to-many relationship between users and skillsets is fully functional.
Sub-tasks:
Test the integration in creating and updating users with associated skillsets.
Verify the integrity of data when deleting users or skillsets.
Testing: Conduct integration tests to check the relational data integrity.
4. Client-Side Development (Optional/Future Task)
Task: Develop a frontend interface for interacting with the API.
Sub-tasks:
Choose a suitable frontend framework (like Angular or React).
Design and implement UI components for managing users and skillsets.
Testing: Test the frontend application's integration with the backend API.
5. Implement Additional Features
Task: Add advanced features to the API.
Sub-tasks:
Implement security measures like authentication and authorization.
Develop caching strategies to improve performance.
Add pagination for handling large datasets.
Create a comprehensive testing strategy.
Research and set up a CI/CD pipeline for automated deployment.
6. Prepare for Hosting and Deployment
Task: Get the application ready for deployment.
Sub-tasks:
Research hosting platforms like Heroku or AWS.
Prepare the application for deployment, including environment-specific configurations.
Testing: Test the application in a staging environment to ensure it's ready for production.
7. Documentation and Repository Management
Task: Keep the documentation and repository updated.
Sub-tasks:
Continuously update the README file with the latest project changes.
Maintain clear and descriptive commit messages in the Git repository.
Testing: Review the documentation for clarity and completeness.
