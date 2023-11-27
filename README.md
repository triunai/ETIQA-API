# 🌐 Complete Developer Network (CDN) - Freelancer Directory API

## 🌟 Introduction
Welcome to the CDN Freelancer Directory API! This RESTful API is a pivotal component of the Complete Developer Network, designed to streamline the management of a dynamic directory of freelancers. It's not just a project; it's a tool that empowers users to efficiently handle user data and skillsets, fostering a vibrant and interconnected freelance community.

## 🌟 Features
- **User Management**: Create, update, delete, and retrieve user profiles.
- **Skillset Management**: Manage skillsets associated with users.
- **Image Management**: Upload and retrieve profile images.

## 📡 API Endpoints

### 🖼️ ImagesController
- **Upload Image**
  - `POST /api/images/upload`
  - Description: Uploads a new profile image.
  - Request/Response Format: ...
  - URL: `http://<your-domain>/api/images/upload`
- **Get All Images**
  - `GET /api/images`
  - Description: Retrieves all uploaded images.
  - Response Format: ...
  - URL: `http://<your-domain>/api/images`

### 🛠️ SkillsetController
- **Create Skillset**
  - `POST /api/skillsets`
  - Description: Creates a new skillset.
  - Request/Response Format: ...
  - URL: `http://<your-domain>/api/skillsets`
- **Update Skillset**
  - `PUT /api/skillsets/{id}`
  - Description: Updates an existing skillset.
  - Request/Response Format: ...
  - URL: `http://<your-domain>/api/skillsets/{id}`
- **Delete Skillset**
  - `DELETE /api/skillsets/{id}`
  - Description: Deletes a skillset.
  - Request Format: ...
  - URL: `http://<your-domain>/api/skillsets/{id}`
- **Get Skillset**
  - `GET /api/skillsets/{id}`
  - Description: Retrieves a specific skillset.
  - Response Format: ...
  - URL: `http://<your-domain>/api/skillsets/{id}`
- **Get All Skillsets**
  - `GET /api/skillsets`
  - Description: Retrieves all skillsets.
  - Response Format: ...
  - URL: `http://<your-domain>/api/skillsets`

### 🧑‍💼 User Management (note the base url, set it to whatever you may like)
- **Create Users (POST)**: `http://localhost:5000/api/users`
- **Update Users (PUT)**: `http://localhost:5000/api/users/{id}`
- **Delete Users (DELETE)**: `http://localhost:5000/api/users/{id}`
- **Get Users (GET)**: `http://localhost:5000/api/users`
- **Get User by ID (GET)**: `http://localhost:5000/api/users/{id}`

## 📊 Models and DTOs
- **User**: Represents a user in the system.
- **Skillset**: Represents a skillset associated with a user.
- **ProfileImage**: Represents a user's profile image.
- **CreateSkillsetRequestDTO**: Data structure for creating a skillset.
- **CreateUserRequestDTO**: Data structure for creating a user.
- **ProfileImageDTO**: Data structure for profile images.
- **SkillsetDTO**: Data structure for skillsets.


## 🛠️ Technologies Used
- **ASP.NET Core Web API**: For robust backend functionality.
- **Entity Framework Core**: Streamlining database operations.
- **SQL Server**: Reliable and scalable data storage.

## 📅 Development Highlights
- **Day 1**: Laid the groundwork with foundational models and CRUD operations for Skillset.
- **Subsequent Days**: Enhanced user management, error handling, and database integration.

## 🚀 Getting Started
- **Clone the Repository**: Access our complete codebase.
- **Configure SQL Server**: Set up your database connection.
- **Run Migrations**: Establish your database schema.
- **Launch the API**: Start interacting with your local instance.

## 🌈 Future Enhancements
- **Client-Side Development**: Introducing a user-friendly interface.
- **Advanced Security**: Implementing robust endpoint protection.
- **Performance Optimization**: Caching and pagination for efficiency.
- **Comprehensive Testing**: Ensuring reliability and stability.
- **Deployment**: Exploring cloud hosting options for wider accessibility.

## 📝 Task Breakdown
- **User Management Module**: Enhancing CRUD operations and integration with skillsets.
- **Skillset Management Features**: Optimizing functionality and error handling.
- **Many-to-Many Relationship**: Testing and verifying data integrity.
- **Client-Side Development**: (Future) Building a frontend interface.
- **Additional Features**: Security, caching, pagination, and testing strategies.
- **Hosting and Deployment**: Preparing for a live environment.
- **Documentation**: Continuously updating for clarity and completeness.
