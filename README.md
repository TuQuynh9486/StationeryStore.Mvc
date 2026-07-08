# Stationery Store MVC - Lab06 Final

## Student Information

- Student: Trần Thị Tú Quỳnh
- Course: ASP.NET Core MVC
- Project: Stationery Store MVC
- Framework: .NET 8
- Database: SQL Server

---

# Project Overview

Stationery Store MVC là hệ thống quản lý văn phòng phẩm sử dụng ASP.NET Core MVC.

Project được phát triển từ Lab01 → Lab06 và bao gồm:

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- Authentication
- Authorization
- Policy-Based Authorization
- Repository Pattern
- Service Pattern
- Soft Delete
- Restore
- RowVersion Concurrency
- Health Check
- Structured Logging
- Audit Log
- Secure Image Upload
- ProblemDetails API

---

# Technologies

- ASP.NET Core 8
- Entity Framework Core
- SQL Server
- Identity
- Razor View
- Bootstrap
- Dependency Injection

---

# Features

## Authentication

- Register
- Login
- Logout

---

## Authorization

Roles

- Admin
- Staff
- User

Policies

- CanViewProduct
- CanManageProduct
- CanUploadProductImage
- CanViewAuditLog

---

## Product Management

- Product List
- Product Detail
- Search
- Create
- Edit
- Delete
- Soft Delete
- Trash
- Restore

---

## Inventory

- Adjust Stock
- Inventory Transaction

---

## Upload

Admin có thể upload hình ảnh sản phẩm.

Các giới hạn:

- jpg
- jpeg
- png
- webp

Maximum size

- 2MB

---

## Audit Log

Hệ thống ghi lại các hành động:

- Login
- Logout
- Register
- Create
- Edit
- Delete
- Restore
- Upload Image

---

## Health Check

- /health/live

- /health/ready

---

## API

Ví dụ

GET

/api/stationery/1

Nếu không tồn tại sẽ trả về ProblemDetails.

---

# Database

## Update Database

```bash
dotnet ef database update
```

---

## Migration

```bash
dotnet ef migrations add FinalLab06

dotnet ef database update
```

---

# Demo Accounts

## Admin

Email

admin@gmail.com

Password

Admin@123

---

## Staff

Email

staff@gmail.com

Password

Staff@123

---

## User

Email

user@gmail.com

Password

User@123

---

# Authorization Test

| Account | View | Search | Create | Edit | Delete | Restore | Upload | Audit |
|---------|------|--------|--------|------|--------|----------|---------|-------|
| Admin | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ | ✅ |
| Staff | ✅ | ✅ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ |
| User | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ | ❌ |
| Anonymous | Redirect Login | Redirect Login | Redirect Login | Redirect Login | Redirect Login | Redirect Login | Redirect Login | Redirect Login |

---

# Security

Implemented

- AntiForgeryToken
- Razor Encoding
- SQL Injection Protection
- Cookie Authentication
- Role Authorization
- Policy Authorization
- Safe Upload
- RowVersion
- Soft Delete

---

# Project Structure

Controllers

- HomeController
- StationeryController
- CategoriesController
- SuppliersController
- InventoryController
- AccountController
- AuditLogsController
- StationeryApiController

Services

- StationeryService
- InventoryService
- CategoryService
- SupplierService
- AuditLogService

Repositories

- StationeryRepository
- InventoryRepository
- CategoryRepository
- SupplierRepository

---

# Running Project

Clone project

```bash
git clone <repository>
```

Restore packages

```bash
dotnet restore
```

Run migration

```bash
dotnet ef database update
```

Run

```bash
dotnet run
```

Open

```
https://localhost:5151
```

---

# Git Branch

main

lab05

final

---

# Screenshots

- Login

- Register
  
- Product List
  
- Search
  
- Create
  
- Edit
  
- Trash
  
- Restore

- Upload Image

- Health Check

- Audit Log

- API ProblemDetails

---

# Test Checklist

- Identity

- Authorization

- Policy

- CRUD

- Validation

- RowVersion

- Soft Delete

- Restore

- Upload Image

- Audit Log

- Health Check

- ProblemDetails

- Logging

- README

All requirements of Lab06 Final have been implemented.
