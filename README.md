# Stationery Store MVC - Lab06 Final

## Student Information

- **Student:** Trần Thị Tú Quỳnh
- **Course:** ASP.NET Core MVC
- **Project:** Stationery Store MVC
- **Framework:** .NET 8
- **Database:** SQL Server

---

# Project Overview

Stationery Store MVC là hệ thống quản lý kho văn phòng phẩm được xây dựng bằng ASP.NET Core MVC.

Ứng dụng hỗ trợ quản lý sản phẩm, danh mục, nhà cung cấp và giao dịch nhập kho. Đồng thời tích hợp ASP.NET Core Identity để xác thực người dùng, phân quyền theo vai trò và áp dụng các kỹ thuật bảo mật trong phát triển ứng dụng web.

Project được phát triển xuyên suốt từ Lab01 đến Lab06 với các kiến thức:

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- Repository Pattern
- Service Pattern
- Dependency Injection
- Authentication & Authorization
- Health Check
- Structured Logging
- Audit Log
- ProblemDetails API
- Secure File Upload

---

# Technologies

- ASP.NET Core 8
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- Razor View
- Bootstrap
- Dependency Injection

---

# Main Features

## Dashboard

Dashboard hiển thị tổng quan hệ thống:

- Tổng số sản phẩm
- Tổng số danh mục
- Tổng số nhà cung cấp
- Tổng số giao dịch kho
- Tổng số Audit Logs
- Security Controls
- Quick Access

---

## Authentication

- Register
- Login
- Logout

---

## Authorization

### Roles

- Admin
- Staff
- User

### Policies

- CanViewProduct
- CanManageProduct
- CanUploadProductImage
- CanViewAuditLog

---

## Product Management

- Product List
- Product Detail
- Search Product
- Create Product
- Edit Product
- Soft Delete
- Trash
- Restore
- RowVersion Concurrency

---

## Category Management

- Category List
- Category Detail

---

## Supplier Management

- Supplier List
- Supplier Detail

---

## Inventory Management

Quản lý các giao dịch nhập kho.

Bao gồm:

- Create Inventory Transaction
- Inventory History
- Inventory Detail

Mỗi giao dịch sẽ:

- Tạo Inventory Record
- Tạo Inventory Detail
- Cập nhật số lượng tồn kho
- Sử dụng Transaction để đảm bảo tính toàn vẹn dữ liệu

---

## Upload Product Image

Admin có thể upload hình ảnh sản phẩm.

Điều kiện:

- Allowed extensions:
  - jpg
  - jpeg
  - png
  - webp

- Maximum size:
  - 2 MB

---

## Audit Log

Hệ thống lưu lại các hành động quan trọng:

- Register
- Login
- Logout
- Create Product
- Edit Product
- Soft Delete
- Restore
- Upload Image

Thông tin được lưu:

- User
- Action
- Entity
- EntityId
- Result
- Description
- TraceId
- RequestPath
- IP Address
- Created Time

---

## Health Check

Application hỗ trợ:

- `/health/live`
- `/health/ready`

---

## API

Ví dụ:

```
GET /api/stationery/{id}
```

Nếu không tìm thấy dữ liệu sẽ trả về ProblemDetails gồm:

- Title
- Status
- Detail
- Instance
- TraceId
- ErrorCode

---

# Installation

## Clone Project

```bash
git clone <repository-url>
```

---

## Restore Packages

```bash
dotnet restore
```

---

## Build Project

```bash
dotnet build
```

---

## Database Migration

Nếu chưa có database:

```bash
dotnet ef database update
```

Hoặc tạo migration mới:

```bash
dotnet ef migrations add FinalLab06

dotnet ef database update
```

---

## Run Application

```bash
dotnet run
```

Mặc định ứng dụng chạy tại:

```
https://localhost:5151
```

---

# Demo Accounts

| Role | Email | Password |
|------|------|------|
| Admin | admin@gmail.com | Admin@123 |
| Staff | staff@gmail.com | Staff@123 |
| User | user@gmail.com | User@123 |

---

# Authorization

| Feature | Admin | Staff | User | Anonymous |
|---------|------|------|------|------|
| Dashboard | ✅ | ✅ | ❌ | Redirect Login |
| Product List | ✅ | ✅ | ❌ | Redirect Login |
| Product Detail | ✅ | ✅ | ❌ | Redirect Login |
| Search Product | ✅ | ✅ | ❌ | Redirect Login |
| Create Product | ✅ | ❌ | ❌ | Redirect Login |
| Edit Product | ✅ | ❌ | ❌ | Redirect Login |
| Delete Product | ✅ | ❌ | ❌ | Redirect Login |
| Restore Product | ✅ | ❌ | ❌ | Redirect Login |
| Upload Image | ✅ | ❌ | ❌ | Redirect Login |
| Inventory Transaction | ✅ | ✅ | ❌ | Redirect Login |
| Audit Log | ✅ | ❌ | ❌ | Redirect Login |

---

# Security Features

Đã triển khai:

- ASP.NET Core Identity
- Cookie Authentication
- Role-Based Authorization
- Policy-Based Authorization
- ValidateAntiForgeryToken
- Razor Encoding
- SQL Injection Protection
- Secure File Upload
- RowVersion Concurrency
- Soft Delete
- Audit Log
- Structured Logging
- Health Check
- ProblemDetails API

---

# Project Structure

## Controllers

- HomeController
- AccountController
- StationeryController
- CategoriesController
- SuppliersController
- InventoryController
- AuditLogsController
- StationeryApiController

## Services

- StationeryService
- CategoryService
- SupplierService
- InventoryService
- AuditLogService

## Repositories

- StationeryRepository
- CategoryRepository
- SupplierRepository
- InventoryRepository
- AuditLogRepository

---

# Git Branches

- main
- lab05
- final

---

# Notes

- Chỉ **Admin** được phép tạo, chỉnh sửa, xóa, khôi phục sản phẩm và upload hình ảnh.
- **Staff** chỉ có quyền xem dữ liệu và thực hiện giao dịch nhập kho.
- **Anonymous** sẽ được chuyển về trang Login khi truy cập các chức năng yêu cầu xác thực.
- Ứng dụng sử dụng Transaction trong nghiệp vụ nhập kho để đảm bảo dữ liệu luôn nhất quán khi có lỗi xảy ra.
