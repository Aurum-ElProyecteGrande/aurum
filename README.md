# Aurum Documentation

## Project Overview
**Name:** Aurum  

**Description:**  
Aurum is a personal expense tracker web application showcasing a Next.js frontend, connected to a ASP.NET backend, using an MSSQL database.
The app was made by three of us as a team project - our workflow was based on SCRUM.
We had 5 sprints, daily stand-up meetings to split the tasks, used project board and task delegation.
During development we followed SOLID principles and Clean Code methodology, implemented CI in github actions.
The product runs in Docker containers.

**App demo:**
//youtube?
//.gif?

##  1. Main features: 
- Register user
- Add accounts for user
- Add expenses / incomes to accounts
- Check data in various customizable charts
- Sort dashboard by different layouts
- List - sort - filter transactions
- Manage accounts
- Manage user
- Responsive mobile view

## 2. Technology Stack
- **Frontend:** React, Next.js, sass
- **Backend:** ASP.NET Core, C#
- **Database:** MSSQL (Entity Framework)
- **Containerization:** Docker
- **Version Control:** Git, GitHub
 
## 3. Installation and Running

- **Github repo:** https://github.com/Aurum-ElProyecteGrande/aurum/

### 3.1. Prerequisites
- Node.js & npm
- .NET SDK
- MSSQL

### 3.2 Enviroment variables
- **RawSeedDataPath** *sets path for the folder with .csv files for the seeder in docker (default: "/app/raw-seeding-data")
- **Database:ConnectionString**
- **Database:DbPassword**
- **Token:Issuer**
- **Token:Audience**
- **Token:Key**

### 3.2. Starting the Backend
```sh
cd Backend
cd Aurum
dotnet restore
dotnet run
```

### 3.3. Starting the Frontend
```sh
cd Frontend
npm install
npm run dev
```

### 3.4. Initialize the Database
```
dotnet ef database update
```
- integrated dataseeding sets up up-to-date 

### 3.5 External dependencies
- https://freecurrencyapi.com/docs/currencies#request-parameters

## 4. Usage Examples

### 4.1. Controllers / API Endpoints
- **AccountController** 
	*/account GET -> GetAll()
	*/account POST -> Create()
	*/account/{accId} PUT -> Update()
	*/account/{accId} Delete -> Delete()
- **BalanceController**
	*/balance/{accId} GET -> GetBalance()
	*/balance/{accId/range} GET -> GetBalanceForRange()
- **CurrencyController**
	*/currency GET -> GetAll()
- **ExpenseController**
	*/expenses/{accId} GET -> GetAll()
	*/expenses/{accId}/{startDate}/{endDate} GET -> GetAllWithDate()
	*/expenses POST -> Create()
	*/expenses/{expenseId} DELETE -> Delete()
- **IncomeController**
	*/income/{accId} GET -> GetAll()
	*/income POST -> Create()
	*/income/{incomeId} DELETE -> Delete()	
- **LayoutController**
	*/layout/basic POST -> CreateBasicLayout()
	*/layout/scientic POST -> CreateScienticLayout()
	*/layout/detailed POST -> CreateDetailedLayout()
	*/layout/{userId} GET -> GetAll()
- **UserController**
	*/user GET -> GetUserInfo()
	*/user PUT -> Update()
	*/user DELETE -> Delete()
	*/user/register POST -> Register()
	*/user/login POST -> Authenticate()
	*/user/password-change PUT -> PasswordChange()
	*/user/validate GET -> Validate()
	*/user/logout POST -> Logout()

## 5. Architecture
The project is built with a **RESTful API backend** and a **component-based frontend**.

## 6. Database Structure (SQL)
![database-structure](./database-structure.png)

## 7. Contribution
- **Fork the repo**
- **Create a branch**
- **Submit a PR**

## 8. Contact
Developers: **[Bartos Gábor, Fekete Nándor, Gelecsák Tamás]**  
Emails: **[aurumelproyectegrande@gmail.com]**  
LinkedIns: **[https://www.linkedin.com/in/gaborbartos731, https://www.linkedin.com/in/nándor-fekete-fn97, https://www.linkedin.com/in/tamasgelecsak]**
