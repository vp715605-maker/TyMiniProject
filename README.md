# 🎓 FeePortal - Student Fee Management System

A simple and user-friendly web application built to help educational institutions manage student fees efficiently. It supports student registration, login, profile management, fee tracking, and admin-side fee submission.

---

## 👥 User Roles

- 🧑‍🎓 **Student**
  - Register and log in
  - View profile and fee history
  - Track paid and pending fees
  - Download fee receipts

- 💼 **Cashier/Admin**
  - View all student profiles
  - Create fee entries (e.g., tuition, hostel, exam)
  - Submit fee payments
  - Generate receipts
  - Real-time updates to student dashboards

---

## ✨ Features

- 🔐 Secure login and registration
- 📋 Student dashboard with fee tracking
- 🧾 Admin dashboard for fee management
- ⚡ Instant fee status updates
- 🔒 Role-based access control

---

## 🧰 Tech Stack

| Layer       | Technology                     |
|------------|---------------------------------|
| Frontend   | Razor Views, Bootstrap 5, jQuery |
| Backend    | ASP.NET Core MVC (.NET 6/7), Entity Framework Core |
| Database   | SQL Server                      |
| Auth       | ASP.NET Identity (Student, Admin, Cashier roles) |
| Deployment | IIS or Azure App Service        |

---

## 📁 Project Structure
FeePortal/
├── Controllers/
│   ├── StudentController.cs
│   ├── AdminController.cs
│   └── AccountController.cs
├── Models/
│   ├── Student.cs
│   ├── Fee.cs
│   ├── Payment.cs
│   └── ApplicationUser.cs
├── Views/
│   ├── Student/
│   ├── Admin/
│   └── Shared/
├── Data/
│   └── ApplicationDbContext.cs
├── wwwroot/
│   ├── css/
│   ├── js/
│   └── images/
└── Startup.cs / Program.cs
