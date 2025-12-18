// using System.Diagnostics;
// using Pulse.API.Infrastructure.Persistence;

// namespace Pulse.API.Infrastructure.Logging;

// /// <summary>
// /// Middleware that logs HTTP access and performance data to the database.
// /// </summary>
// public class RequestLoggingMiddleware
// {
//     private readonly RequestDelegate _next;

//     public RequestLoggingMiddleware(RequestDelegate next)
//     {
//         _next = next;
//     }

//     public async Task InvokeAsync(
//         HttpContext context,
//         PulseDbContext db)
//     {
//         Stopwatch? stopwatch = Stopwatch.StartNew();

//         try
//         {
//             await _next(context);
//         }
//         finally
//         {
//             stopwatch.Stop();

//             // CompanyId may be set later by authentication middleware
//             Guid? companyId = null;

//             if (context.Items.TryGetValue("CompanyId", out var value) &&
//                 value is Guid guid)
//             {
//                 companyId = guid;
//             }

//             AccessLog? log = new AccessLog
//             {
//                 Id = Guid.NewGuid(),
//                 Timestamp = DateTimeOffset.UtcNow,
//                 Method = context.Request.Method,
//                 Path = context.Request.Path,
//                 StatusCode = context.Response.StatusCode,
//                 IpAddress = context.Connection.RemoteIpAddress?.ToString(),
//                 CompanyId = companyId,
//                 DurationMs = stopwatch.ElapsedMilliseconds
//             };

//             db.AccessLogs.Add(log);
//             await db.SaveChangesAsync();
//         }
//     }
// }
