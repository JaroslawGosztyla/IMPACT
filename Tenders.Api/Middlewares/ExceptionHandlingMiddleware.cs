namespace Tenders.Api.Middlewares;

public class ExceptionHandlingMiddleware : IMiddleware
{
    public ExceptionHandlingMiddleware(){}

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception e)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Something went wrong");
        }
    }
}
