# HandMade E-commerce Project

Features

1. Identity Authentication for Users and Admin
    User Registration and Login: Secure sign-up and login functionality for customers and admin roles using the Identity framework.
    Role-based Authorization: Users and Admins have different permissions. Admin can manage categories, products, and orders.

2. Admin Management of Categories and Products
      Category Management: Admins can create, edit, and delete product categories.
      Product Management: Admins can add, update, and remove products, along with managing product details like price, stock, and descriptions.
      Track Order Status: The admin can track the user order status (e.g., "Pending," "Shipped," or "Canceled").
   
4. Create New User Roles
      Role Assignment: Admins can create new roles (e.g., "Editor")

6. User Shopping Cart and Payment Integration
     Shopping Cart: Users can add, remove, and update products in their shopping cart.
     Stripe Integration: Payment processing through Stripe is used to handle user payments securely.
    
8. Wishlist
    Add to Wishlist: Users can add items to their wishlist for future purchases.


Technologies and Techniques

1- Asp.net core 8 MVC, Entity Framework Core 8
2- 3 n-tier architecture, Generic Repository pattern, and Unit OF Work, Areas
2- Stripe Payment Integration
3- HTML CSS Bootstrap Javascript 



Installation
-- Clone the repository
-- Set up the database --> update-database
-- Configure app settings --> for your payment keys
-- Run the application
