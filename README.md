I created this project to refresh/learn C#, and how to build an API in both minimal API and controller-based API.

GET		List Employees			/employees
GET		List Active Employees	/employees/active
GET		Get Employee			/employees/{id}
POST	Create Employee			/employees			Name,Age,IsActive
DELETE	Delete Employee			/employee/{id}
PUT		Update Employee			/employee/{id}		Name,Age,IsActive
GET		Root					/