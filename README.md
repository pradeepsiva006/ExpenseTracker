# ExpenseTracker
Expense Tracker API that allows users to record their daily expenses.

# API Documentation

## AuthController

### Register User

**Endpoint:** `POST /api/auth/register`

Registers a new user.

**Request:**
```json
{
  "Name": "John Doe",
  "Password": "password"
}
```

**Response:**
- `200 OK` on successful registration.
```json
{
  "UserId": 1,
  "Token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```
- `400 Bad Request` if user registration fails.

### Login User

**Endpoint:** `POST /api/auth/login`

Logs in an existing user.

**Request:**
```json
{
  "Name": "John Doe",
  "Password": "password"
}
```

**Response:**
- `200 OK` on successful login.
```json
{
  "UserId": 1,
  "Token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```
- `400 Bad Request` if login fails.

### Health Check (Ping)

**Endpoint:** `GET /api/auth/ping`

Performs a health check.

**Response:**
- `200 OK` on successful ping.
```json
{
  "Message": "Ping Successful!"
}
```

## ExpenseController

### Add Expense

**Endpoint:** `POST /api/expense/addExpense`

Adds a new expense for the authenticated user.

**Request:**
```json
{
  "Amount": 100.50,
  "Category": "Groceries",
  "Date": "2024-01-20",
  "ShortDescription": "Weekly grocery shopping"
}
```

**Response:**
- `200 OK` on successful expense addition.
```json
{
  "Message": "Expense Added!"
}
```
- `400 Bad Request` if adding expense fails.
- `500 Server Error` in any other exception with valid message.
### Update Expense

**Endpoint:** `PUT /api/expense/updateExpense`

Updates an existing expense for the authenticated user.

**Request:**
```json
{
  "ExpenseId": 1,
  "UserId": 1,
  "Amount": 120.75,
  "Category": "Groceries",
  "Date": "2024-01-20",
  "ShortDescription": "Monthly grocery shopping"
}
```

**Response:**
- `200 OK` on successful expense update.
```json
{
  "Message": "Expense Updated!"
}
```
- `400 Bad Request` if updating expense fails.
- `500 Server Error` in any other exception with valid message.

### Get All Expenses

**Endpoint:** `GET /api/expense/getAllExpenses`

Retrieves all expenses for the authenticated user.

**Response:**
- `200 OK` on successful retrieval.
```json
{
  "Expenses": [
    {
      "ExpenseId": 1,
      "Amount": 100.50,
      "Category": "Groceries",
      "Date": "2024-01-20",
      "ShortDescription": "Weekly grocery shopping"
    },
    // More expenses...
  ]
}
```
- `400 Bad Request` if retrieval fails.
- `500 Server Error` in any other exception with valid message.
