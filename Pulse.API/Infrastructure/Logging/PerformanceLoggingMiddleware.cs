using System.Diagnostics;
using System.Security.Claims;
using Pulse.API.Application.Logging;
using Pulse.API.Domain.Logging;

namespace Pulse.API.Infrastructure.Logging;

public sealed class PerformanceLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public PerformanceLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext ctx, IPlatformPerformanceLogger performanceLogger)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        int? statusCode = null;
        bool success = false;

        try
        {
            await _next(ctx);
            success = ctx.Response.StatusCode < 500;
            statusCode = ctx.Response.StatusCode;
        }
        catch
        {
            success = false;
            statusCode = 500;
            throw;
        }
        finally
        {
            stopwatch.Stop();

            Guid? userId = null;
            string? sub = ctx.User?.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? ctx.User?.FindFirstValue("sub");

            if (Guid.TryParse(sub, out Guid parsed))
            {
                userId = parsed;
            }

            string? guildId = null;
            if (ctx.Request.RouteValues.TryGetValue("guildId", out object? gObj))
            {
                guildId = gObj?.ToString();
            }

            PlatformPerformanceLog log = new PlatformPerformanceLog
            {
                Id = Guid.NewGuid(),
                Timestamp = DateTimeOffset.UtcNow,
                Source = "API",
                Method = ctx.Request.Method,
                Path = ctx.Request.Path.ToString(),
                StatusCode = statusCode,
                DurationMs = stopwatch.ElapsedMilliseconds,
                Success = success,
                UserId = userId,
                GuildId = guildId
            };

            try
            {
                await performanceLogger.LogAsync(log);
            }
            catch
            {
                
            }
        }
    }
}