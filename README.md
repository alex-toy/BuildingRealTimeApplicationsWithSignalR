# Building Real-Time Applications With SignalR

SignalR is a library to work with in .NET. You can add real-time functionality to your application for notifications, chat, and async data updates. SignalR is also easy to work with since there's just one concept - the Hub class. And everything builds on top of it.



## SignalR in ASP.NET Core Projects

### Nuget Packages
```
Microsoft.EntityFrameworkCore.Tools
```

### Database
```
Add-Migration init
Update-Database
```

### Run App
<img src="/pictures/call.png" title="call center"  width="900">




## Building Real-Time Applications With SignalR & .NET 7

### Nuget Packages
```
Microsoft.AspNetCore.SignalR.Client
```

### Connect the client to Hub

- url
```
wss://localhost:7093/chat-hub
```

- message
```
{
    "protocol : "json",
    "version : 1
}
```
<img src="/pictures/connect.png" title="connect client to hub"  width="900">

### Send message to Hub

- url
```
wss://localhost:7093/chat-hub
```

- message
```
{
    "arguments" : ["a1"],
    "invocationId" : 0,
    "target" : "SendMessage",
    "type" : 1
}
```
<img src="/pictures/connect.png" title="connect client to hub"  width="900">