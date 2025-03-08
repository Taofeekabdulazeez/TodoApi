using System.Text;
using System.Text.Json;
using TodoApi.Dtos;

namespace TodoApi.Middlewares;
public class LoggerMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task Invoke(HttpContext context)
    {
        var request = context.Request;
        var response = context.Response;
        var body = await GetRequestBody<TodoDTO>(context);

        Console.WriteLine($"Request: {request.Method} {request.Path}");
        Console.WriteLine($"Response: {response.StatusCode}");
        Console.WriteLine($"Body: {body}");

        await _next(context);
    }


    private static async Task<T?> GetRequestBody<T>(HttpContext context)
    {
        context.Request.EnableBuffering();
        using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
        var body = await reader.ReadToEndAsync();
        context.Request.Body.Position = 0;

        try
        {
            T? requestObject = JsonSerializer.Deserialize<T>(body, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return requestObject;
        }
        catch (JsonException e)
        {
            Console.WriteLine($"Error deserializing request body: {e.Message}");
            return default;
        }

    }
}
