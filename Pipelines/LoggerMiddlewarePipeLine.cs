using TodoApi.Middlewares;

namespace TodoApi.Pipelines;

public class LoggerMiddlewarePipeLine
{
    public void Configure(IApplicationBuilder app)
    {
        app.UseMiddleware<LoggerMiddleware>();
    }
}