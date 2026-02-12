## Intro
I created this project to refresh/learn C#, and how to build an API in both minimal API and controller-based API.

## Endpoints 
| HTTP | Action | Path | Body |
|---|---|---|---|
| GET | List Employees | /employees | |
| GET | List Active Employees | /employees/active | |
| GET | Get Employee | /employees/{id} | |
| POST | Create Employee | /employees | ```{"Name": "name","Age":"age","IsActive":true}``` |
| DELETE | Delete Employee | /employee/{id} | |
| PUT | Update Employee | /employee/{id} | ```{"Name": "name","Age":"age","IsActive":true}``` |
| GET | Root | / | |
| POST | Authenticate | /auth/login | ```{"email": "sample@company.com","password":"supersecret"}``` |

## Features
- Get/Create/Update/Delete Employees
- Authentication using JWT
- Live updates using SignalR to Blazor frontend

## Test user data
```
Admin - Read/Create/Edit/Delete
email: jsmith@company.com
password: adminSecret
```
```
User - Read Only
email: rsmith@company.com
password: userSecret
```