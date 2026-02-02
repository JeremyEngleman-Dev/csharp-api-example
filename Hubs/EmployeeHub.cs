using Microsoft.AspNetCore.SignalR;

public class EmployeeHub : Hub
{
    // Server can call Clients.All.SendAsync("EmployeeAdded", newEmployee)
}
