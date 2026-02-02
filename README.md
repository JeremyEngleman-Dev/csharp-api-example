## Intro
I created this project to refresh/learn C#, and how to build an API in both minimal API and controller-based API.

## Endpoints 
| HTTP | Action | Path | Body |
|---|---|---|---|
| GET | List Employees | /employees | |
| GET | List Active Employees | /employees/active | |
| GET | Get Employee | /employees/{id} | |
| POST | Create Employee | /employees | JSON |
| DELETE | Delete Employee | /employee/{id} | |
| PUT | Update Employee | /employee/{id} | JSON |
| GET | Root | / | |

#### JSON Structure for employee:
```json
{
    "Name": "name",
    "Age":"age",
    "IsActive":true
}
```

## Features
- Get/Create/Update/Delete Employees
- Live updates using SignalR to Blazor frontend