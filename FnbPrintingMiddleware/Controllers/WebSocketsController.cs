using Infrastructure.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Printing.Domain.Models;
using System.Net.WebSockets;

namespace FnbPrintingMiddleware.Controllers
{
    public class WebSocketsController : BaseController
    {
        private readonly ILogger<WebSocketsController> _logger;
        private NotificationMessageHandler _webSocketHandler;


        public WebSocketsController(
            ILogger<WebSocketsController> logger,
            IMediator mediator,
            NotificationMessageHandler webSocketHandler) : base(logger, mediator)

        {
            _webSocketHandler = webSocketHandler;
        }

        [HttpGet("/ws")]
        public async Task Get([FromQuery] string kioskid, double totalPayable)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                //using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
                //_logger.Log(LogLevel.Information, "WebSocket connection established");
                //await Echo(webSocket);
                var context = HttpContext;
                var socket = await context.WebSockets.AcceptWebSocketAsync();
                await _webSocketHandler.OnConnected(socket, kioskid);
                var payment = new PrintingDto()
                {
                    KioskId = kioskid,
                    TotalPayable = totalPayable
                };

               // var paymentResponse = await SendCommandAsync(new MakePaymentCommand(payment));

                await _webSocketHandler.OnDisconnected(socket);

                await Receive(socket, async (result, buffer) =>
                {
                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        await _webSocketHandler.ReceiveAsync(socket, result, buffer);
                        return;
                    }

                    else if (result.MessageType == WebSocketMessageType.Close)
                    {
                        await _webSocketHandler.OnDisconnected(socket);
                        return;
                    }

                });
            }
            else
            {
                HttpContext.Response.StatusCode = 400;
            }

        }

        private async Task Receive(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer),
                                                        cancellationToken: CancellationToken.None);

                handleMessage(result, buffer);
            }
        }

        private async Task Echo(WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), System.Threading.CancellationToken.None);
            _logger.Log(LogLevel.Information, "Message received from Client");

            while (!result.CloseStatus.HasValue)
            {
                var serverMsg = System.Text.Encoding.UTF8.GetBytes($"Server: Hello. You said: {System.Text.Encoding.UTF8.GetString(buffer)}");
                await webSocket.SendAsync(new ArraySegment<byte>(serverMsg, 0, serverMsg.Length), result.MessageType, result.EndOfMessage, System.Threading.CancellationToken.None);
                _logger.Log(LogLevel.Information, "Message sent to Client");

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), System.Threading.CancellationToken.None);
                _logger.Log(LogLevel.Information, "Message received from Client");

            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, System.Threading.CancellationToken.None);
            _logger.Log(LogLevel.Information, "WebSocket connection closed");
        }


    }
}
