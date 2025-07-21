🛒 Product Inventory System

This is a complete **Product Inventory Management System** built with **ASP.NET Core MVC**, **Entity Framework Core**, and **jQuery with Bootstrap modals** for a modern,
responsive experience. It supports full **CRUD operations** for both Products and Categories using **Repository Pattern** and **API Integration**.

---

✨ Features

- 🔍 View, Add, Edit, Delete Products and Categories
- 📁 Organized using Repository Pattern
- ⚡ Responsive UI with Bootstrap
- 💬 AJAX + jQuery for modals (no full page reloads)
- 📡 API and MVC separated cleanly
- 📦 Entity Framework Core (Database-First)

---

## 🏗️ Tech Stack

| Layer            | Technology                 |
|------------------|----------------------------|
| Frontend (MVC)   | ASP.NET Core MVC           |
| Backend (API)    | ASP.NET Core Web API       |
| Database Access  | Entity Framework Core      |
| UI               | Bootstrap 5, jQuery        |
| Architecture     | Repository Pattern         |

---

## 📁 Project Structure

ProductInventorySystem/
├── Controllers/
│ ├── MvcControllers/ # MVC Controllers for Views
│ └── ApiControllers/ # API Endpoints
├── Models/
│ ├── Product.cs
│ ├── Category.cs
│ └── ViewModels/
├── Repositories/
│ ├── IProductRepository.cs
│ ├── ProductRepository.cs
│ └── CategoryRepository.cs
├── Views/
│ ├── Product/
│ └── Category/
└── wwwroot/
