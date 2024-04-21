using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Gateway
{
    public class Calculation
    {
        private readonly RequestDelegate _next;

        public Calculation(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var startTime = DateTime.UtcNow;

            await _next(context);

            var endTime = DateTime.UtcNow;
            var executionTime = (endTime - startTime).TotalSeconds;
            Console.WriteLine($"Request for {context.Request.Path} has been completed in {executionTime} seconds.");
        }
    }
    public static class CalculationExtensions
    {
        public static IApplicationBuilder Calculation(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<Calculation>();
        }
    }

}