using SignalR.API.Hubs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("broadcast", ChatHub.SendMessageToAll());

app.UseHttpsRedirection();

app.MapHub<ChatHub>("chat-hub");

app.Run();
