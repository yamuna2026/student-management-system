# 🚀 Student Management System

A full-stack, cloud-enabled application built using **.NET 8, React, and Microsoft Azure**, designed to manage student data with secure authentication and scalable architecture.

---

## 🧠 Tech Stack

* **Backend:** ASP.NET Core Web API (.NET 8)
* **Frontend:** React.js (JavaScript, HTML, CSS)
* **Database:** SQL Server (Entity Framework Core)
* **Cloud Services:**

  * Azure Functions (HTTP Trigger & Timer Trigger)
  * Azure Service Bus (Event-driven communication)
* **Authentication:** JWT (Role-Based Authorization)
* **Tools:** Git, GitHub, Postman, Swagger

---

## 🔥 Key Features

* 🔐 Secure **JWT Authentication & Authorization**
* 👥 Role-based access (**Admin & User**)
* 📊 Full **CRUD Operations** for student management
* ⚡ **Serverless APIs** using Azure Functions
* ⏱️ **Scheduled background processing** using Timer Trigger
* 🔄 **Event-driven architecture** using Azure Service Bus
* 🌐 RESTful API integration with React frontend

---

## ☁️ Cloud & Architecture

This project follows a **modern scalable architecture**:

Frontend (React)
↓
ASP.NET Core API / Azure Functions
↓
Service Layer (Managers)
↓
SQL Server Database

Additionally:

* Azure Functions handle **serverless operations**
* Azure Service Bus enables **asynchronous communication**

---

## 📁 Project Structure

student-management-system/
│
├── StudentManagementAPI        # Backend API + Azure Functions
├── student-management-frontend # React Frontend

---

## ▶️ How to Run Locally

### 🔹 1. Backend

* Open `StudentManagementAPI`
* Run the project (Visual Studio / `dotnet run`)

### 🔹 2. Frontend

cd student-management-frontend
npm install
npm start

### 🔹 3. Azure Functions

* Run Azure Functions project
* Test endpoints via browser/Postman

---

