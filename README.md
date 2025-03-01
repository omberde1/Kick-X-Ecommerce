# Kick X - Shoe Ecommerce Store

Kick X is an e-commerce platform for buying and selling shoes. It features two main user roles: **Admin** and **Customer**, each with distinct functionalities.

## Features

### Admin Features
- **Login**: Admins can log in using their credentials.
- **Authorization**: Only admins can access the Admin Controller.
- **Category Management**:
  - Add, edit, or delete product categories (e.g., Nike, Adidas).
- **Product Management**:
  - Add, edit, or delete products.
- **Order Management**:
  - View and edit order statuses (e.g., Pending, Shipped, Delivered).
- **Dashboard**:
  - Analyze data such as recent orders, total sales, and pending orders.

### Customer Features
- **Browse Products**: Customers can browse products without logging in.
- **Authorization**: Certain actions (e.g., adding to cart, placing orders) require login.
- **Cart**:
  - Add/remove products to/from the cart.
- **Orders**:
  - View order history and track order status.

## Technologies Used
- **Backend**: ASP.NET Core (.NET 8)
- **Frontend**: HTML, CSS, JavaScript, Tailwind CSS
- **Database**: MSSQL Server w/ Entity Framework Core (Code-First Approach)
- **Authentication**: Claims-based Authorization

## How to Run the Project

### Prerequisites
- .NET SDK (version 6.0 or higher)
- SQL Server (or any database supported by EF Core)
- Visual Studio or Visual Studio Code

### Steps
1. Clone the repository:
    ```bash
   git clone https://github.com/omberde1/Kick-X-Ecommerce.git
2. Open the project in Visual Studio or Visual Studio Code.
3. Update the database connection string in appsettings.json.
4. Run the following commands to apply migrations and seed the database:
    ```bash
   dotnet ef database update
5. Run the project:
   ```bash
   dotnet run
6. Open your browser and navigate to
   ```bash
   https://localhost:5001

## Screenshots :
### Admin :
- **Login**
![AdminLogin](https://github.com/user-attachments/assets/a9189c44-1dbd-4222-8c80-698bae8a802f)
- **Dashboard (Home)**
![AdminDashboard](https://github.com/user-attachments/assets/79be85f7-4c2c-4d79-9c6b-72537c53c692)
- **All Customers**
![AdminAllCustomers](https://github.com/user-attachments/assets/cbdd8507-2c8f-4a19-a708-70c6ca777307)
- **All Categories (w/ CRUD operations)**
![AdminAllCategory](https://github.com/user-attachments/assets/4b248902-04ba-4c5f-a59f-cab0803c22e4)
- **All Products (w/ CRUD operations)**
![AdminAllProducts](https://github.com/user-attachments/assets/b6c2992d-3f4d-4176-bebf-002aab639565)
- **All Orders (w/ Edit Order Status)**
![AdminAllOrders](https://github.com/user-attachments/assets/1792fd85-d4d4-47b5-80de-4ef21c6377ea)
- **Toogle Theme (Dark/Light)**
![AdminToggleTheme](https://github.com/user-attachments/assets/21a253da-6761-403f-8838-88a0e2e140a0)
- **Logout**
![AdminLogout](https://github.com/user-attachments/assets/37b954b2-f402-4ca7-b631-9e8a62a7fb4e)

### A) Customer (Not Logged In):
- **Home #1**
![KickXHome1_NotLoggedIn](https://github.com/user-attachments/assets/9c4de8d2-816f-4fd7-9d29-3ae1c93f9b3a)
- **Home #2**
![KickXHome2_NotLoggedIn](https://github.com/user-attachments/assets/91f8f21e-f8fb-4b81-ad28-89299098482b)
- **Home #3**
![KickXHome3_NotLoggedIn](https://github.com/user-attachments/assets/f08c97f3-198d-4687-bd35-e1a390b12414)
- **Home #4**
![KickXHome4_NotLoggedIn](https://github.com/user-attachments/assets/7dbc7447-2497-42b8-94a1-4187c17d094c)
- **Store**
![KickXStore1_NotLoggedIn](https://github.com/user-attachments/assets/4d4d5718-c89f-407c-8d73-94e46075e569)
- **Store -> Product Unit**
![KickXStoreUnit1_NotLoggedIn](https://github.com/user-attachments/assets/1b03f11f-5596-41b0-9111-cf0a46071204)
- **Login (When trying to access Profile/Cart options)**
![KickX_Login](https://github.com/user-attachments/assets/4400a265-07ef-4726-9100-65108d7bd520)
- **Register**
![KickX_Register](https://github.com/user-attachments/assets/0cf0fadf-d15a-436b-900c-c7e6686b16ad)

### A) Customer (Logged In):
- **Home #1 (Orders now visible + could now access Profile/Cart)**
![KickXHome_LoggedIn](https://github.com/user-attachments/assets/645d3ac9-2213-441f-ad82-ac52634f5e23)
- **Adding to Cart**
![KickXStoreUnit_LoggedIn](https://github.com/user-attachments/assets/c343a388-e1c7-4c22-9d68-1fd419871f73)
- **Cart**
![KickXCart_LoggedIn](https://github.com/user-attachments/assets/b0905683-2c85-45bb-8c76-be6ba99b7029)
- **Placing Order**
![KickXPlaceOrder_LoggedIn](https://github.com/user-attachments/assets/60cd699c-bc82-4a9b-9f7e-b2598a9af4cb)
- **Order Placed (Success page)**
![KickXOrderPlaced](https://github.com/user-attachments/assets/81a7c775-361e-4401-b0ac-6f4d9985d1e5)
- **Orders History**
![KickXAllOrders_LoggedIn](https://github.com/user-attachments/assets/40818fd4-1192-4002-9ff3-348131d747b6)
- **Order Details (Solo)**
![KickXOrderDetails_LoggedIn](https://github.com/user-attachments/assets/3318c7f8-afbd-4c34-9d88-9d24c9d34f27)
- **Profile Details (Edit/Logout)**
![KickXLogoutPage](https://github.com/user-attachments/assets/2aa04fe8-ac67-427d-b4b6-f286f122ad88)
- **Customer trying to access Admin (Explicitly ofc)**
![CustomerTrynaAccessAdminPanel](https://github.com/user-attachments/assets/c2a387e0-de88-4dd3-91c5-f74d19a0ec2d)

## License
### This project is licensed under the MIT License. See the LICENSE file for details.

## Contact
### For questions or feedback, feel free to reach out:
- Email: omberde0@gmail.com
- My Portfolio (Linktress): https://linktr.ee/omberde0

### PS
- This was my first solo project using ASP.NET Core so there could be some minor flaws present here & there.
- Also, this project is still inprogress and some things are still to be implemented such as pagination, searching, sorting, etc. (Its only included in some parts of the project tho).
- However, any suggestions / contributions are warmly welcomed.
