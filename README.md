# Expense Tracker 🧾

A full-stack **Expense Tracker application** built as a **side project** to practice and demonstrate **real-world backend architecture, authentication, and cloud-ready design** using modern Microsoft technologies.

> ⚠️ **Project Status:** In Progress  
> This project is actively being developed and enhanced in phases.

---

## 🎯 Purpose

This project is created to:
- Apply **clean architecture** principles
- Implement **secure authentication & authorization**
- Practice **enterprise-style API design**
- Gain hands-on experience with **Azure-ready integrations**
- Serve as a **learning + portfolio project**

---

## 🧱 Tech Stack

### Backend
- ASP.NET Core Web API
- Entity Framework Core
- ASP.NET Core Identity
- JWT Authentication
- Clean / Layered Architecture
- Swagger (OpenAPI)

### Frontend
- Angular
- JWT-based API integration

### Cloud & DevOps (Planned / Partial)
- Azure App Service
- Azure Key Vault
- Azure Blob Storage
- Azure Application Insights
- Azure Service Bus (planned)
- Azure Functions (planned)

---

## 🏗️ Architecture Overview

Angular UI  
↓  
ASP.NET Core Web API  
↓  
Controllers → Business Logic (Services) → Repositories → EF Core / Identity  

Cross-cutting concerns:
- Global exception handling middleware
- Request logging middleware
- JWT authentication & role-based authorization

---

## 🔐 Authentication & Authorization

- JWT-based authentication
- Role-based access control
  - Reader
  - Writer
- Secured APIs using `[Authorize]` attributes
- Swagger configured for JWT testing

---

## ✅ Features Implemented So Far

- User registration & login
- JWT token generation & validation
- Role-based authorization
- Category CRUD APIs
- Clean separation of concerns (Controller → Service → Repository)
- EF Core migrations
- Global exception handling
- Request logging middleware

---

## 🚧 Work in Progress / Planned Features

- Expense CRUD APIs
- Income CRUD APIs
- Receipt upload using Azure Blob Storage
- Azure Key Vault integration for secrets
- Monthly expense reports
- Angular UI enhancements
- Azure deployment & monitoring
- Background processing with Azure Functions

---

## ▶️ Running the Project Locally

### Backend
1. Open the solution in Visual Studio
2. Update connection string in `appsettings.json`
3. Run EF Core migrations
4. Run the API
5. Open Swagger at:
   https://localhost:<port>/swagger

### Frontend
1. Navigate to `frontend/expense-tracker-ui`
2. Install dependencies:
   npm install
3. Start Angular app:
   ng serve

---

## 🧪 API Testing

- Swagger UI supports JWT authentication
- Use Login API to generate token
- Click Authorize and pass:
  <JWT_TOKEN>

---

## 📌 Notes

- This is a personal side project
- Built incrementally with a focus on code quality over speed
- Not intended for production use yet
- Designed to reflect real enterprise patterns

---

## 📄 License

This project is for learning and demonstration purposes.

---

## 👤 Author

Developed by **Aniket**  
.NET Core Web API Developer | Azure Enthusiast

---

> 💡 This repository reflects ongoing learning and continuous improvement.
