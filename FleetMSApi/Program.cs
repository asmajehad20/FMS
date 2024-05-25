
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);
ConcurrentDictionary<string, WebSocket> _webSockets = new ConcurrentDictionary<string, WebSocket>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{

    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();

    });
});


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();
app.UseCors();
app.UseRouting();
app.MapControllers();
app.UseWebSockets();


app.Map("/ws", async context => {

    if (context.WebSockets.IsWebSocketRequest)
    {
        using var ws = await context.WebSockets.AcceptWebSocketAsync();
        string socketId = Guid.NewGuid().ToString();
        _webSockets.TryAdd(socketId, ws);

        while (ws.State == WebSocketState.Open)
        {
            await Receive(ws, socketId);
            Thread.Sleep(1000);
        }
  
    }
    else
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    }
});


app.Run();


 async Task Receive(WebSocket webSocket, string socketId)
{
    var buffer = new byte[1024 * 4];

    
    while (webSocket.State == WebSocketState.Open)
    {
        var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        if (result.MessageType == WebSocketMessageType.Text)
        {
            var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
            Console.WriteLine($"Received: {message}");
            await Broadcast(message);
        }
        else if (result.MessageType == WebSocketMessageType.Close)
        {
            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by client", CancellationToken.None);
            _webSockets.TryRemove(socketId, out _);
        }
    }
}



async Task Broadcast(string msg)
{
    var bytes = Encoding.UTF8.GetBytes(msg);
    var buffer = new ArraySegment<byte>(bytes, 0, bytes.Length);

    foreach (var ws in _webSockets.Values)
    {
        if (ws.State == WebSocketState.Open)
        {
            
            if (ws.State == WebSocketState.Open)
            {
                await ws.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
            }
            
        }
    }
}

