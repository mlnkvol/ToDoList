using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace ToDo.SSE.Controllers
{
    [ApiController]
    [Route("sse")]
    public class SseController : ControllerBase
    {
        private static readonly ConcurrentDictionary<string, IResponseStream> _clients = new();

        [HttpGet]
        public async Task Sse()
        {
            Response.Headers.Add("Content-Type", "text/event-stream");
            var clientId = Guid.NewGuid().ToString();
            var clientStream = new ResponseStream(Response);

            _clients[clientId] = clientStream;

            while (true)
            {
                await clientStream.WriteAsync($"data: ping\n\n");
                await Task.Delay(10000);
            }
        }

        public static void SendUpdate(string message)
        {
            foreach (var client in _clients.Values)
            {
                client.WriteAsync($"data: {message}\n\n").ConfigureAwait(false);
            }
        }
    }

    public interface IResponseStream
    {
        Task WriteAsync(string message);
    }

    public class ResponseStream : IResponseStream
    {
        private readonly HttpResponse _response;

        public ResponseStream(HttpResponse response)
        {
            _response = response;
        }

        public async Task WriteAsync(string message)
        {
            await _response.WriteAsync(message);
            await _response.Body.FlushAsync();
        }
    }
}
