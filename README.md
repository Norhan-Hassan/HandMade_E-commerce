# 🧵 HandMade E-commerce Project

A full-featured handcrafted goods e-commerce website built with **ASP.NET Core 8 MVC**, supporting admin and user roles, product management, Stripe payments, and more.

---
## 📽️ Demo

  [[Watch the Demo]](https://youtu.be/5g2DTWFKvVg)

![Screenshot 2025-04-24 210122](https://github.com/user-attachments/assets/641fb143-ea62-4690-a933-17e5af88afb3)



## ✨ Features

### 🔐 1. Identity Authentication for Users and Admins
- **User Registration and Login**: Secure authentication using the Identity Framework.
- **Role-Based Authorization**:  
  - Users: Can browse, shop, and manage personal features.  
  - Admins: Have full control over categories, products, and order management.

### 🗂️ 2. Admin Management of Categories and Products
- **Category Management**: Create, update, and delete product categories.
- **Product Management**: Full CRUD operations for products with stock, price, and descriptions.
- **Order Tracking**: Track and update order statuses (e.g., *Pending*, *Shipped*, *Cancelled*).

### 👥 3. User Role Management
- **Custom Role Assignment**: Admins can create and assign custom roles (e.g., *Editor*).

### 🛒 4. Shopping Cart & Payment Integration
- **Shopping Cart**: Users can add, remove, and modify cart items.
- **Stripe Integration**: Secure payment processing using [Stripe](https://stripe.com/).

### 💖 5. Wishlist
- **Add to Wishlist**: Users can save favorite items for future purchases.

---

## 🛠️ Technologies & Techniques

- **ASP.NET Core 8 MVC**  
- **Entity Framework Core 8**
- **N-Tier Architecture**
- **Generic Repository & Unit of Work Pattern**
- **Areas for Admin/User Separation**
- **Stripe Payment Integration**
- **Frontend**: HTML, CSS, Bootstrap, JavaScript  
- **DataTables** for dynamic product listing and filtering

---

## 🚀 Installation & Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/your-username/HandMade-Ecommerce.git
   cd HandMade-Ecommerce
