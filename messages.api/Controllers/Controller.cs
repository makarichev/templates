using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace messages.api.Controllers
{
    [ApiController]
    [Route("api")]
    public class Controller : ControllerBase
    {

        private readonly ILogger<Controller> _logger;

        public Controller(ILogger<Controller> logger)
        {
            _logger = logger;
        }

        [HttpGet("connect")]
        public async Task ConnectAsync()
        {

            if (HttpContext.WebSockets.IsWebSocketRequest) {
                var ws = await HttpContext.WebSockets.AcceptWebSocketAsync();
                _logger.LogWarning("socket.open");
                await ws.SendAsync(Encoding.UTF8.GetBytes("Привет"), System.Net.WebSockets.WebSocketMessageType.Text, true, CancellationToken.None);

                while (!ws.CloseStatus.HasValue) {
                    await Task.Delay(1000);
                }

                _logger.LogWarning("socket.close");
                await ws.CloseAsync(System.Net.WebSockets.WebSocketCloseStatus.NormalClosure, "Ok", CancellationToken.None);
            }
            
        }
    }
}
