using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using Gateway_Ocelot.Models;

namespace Gateway_Ocelot.Middlewares
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.EnableBuffering();
            var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
            context.Request.Body.Position = 0;

            // Parse the JSON body
            var requestModel = JObject.Parse(body).ToObject<RequestModel>();

            // Determine the service and path based on the RequestModel
            if (requestModel.Service.Equals("auth", StringComparison.OrdinalIgnoreCase))
            {
                if (requestModel.Action.Equals("register", StringComparison.OrdinalIgnoreCase))
                {
                    context.Request.Path = "/api/auth/register";
                    context.Request.Host = new HostString("localhost", 7182);
                    Console.WriteLine("Register request detected.");
                }
                else if (requestModel.Action.Equals("login", StringComparison.OrdinalIgnoreCase))
                {
                    context.Request.Path = "/api/auth/login";
                    context.Request.Host = new HostString("localhost", 7182);
                    Console.WriteLine("Login request detected.");
                }
            }
            else if (requestModel.Service.Equals("invoices", StringComparison.OrdinalIgnoreCase))
            {
                if (requestModel.Action.Equals("getall", StringComparison.OrdinalIgnoreCase) && context.Request.Method == HttpMethods.Get)
                {
                    context.Request.Path = "/api/invoices/all";
                    context.Request.Host = new HostString("localhost", 7257);
                    Console.WriteLine("Get all invoices request detected.");
                }
                else if (context.Request.Method == HttpMethods.Get)
                {
                    context.Request.Path = $"/api/invoices/{requestModel.Invoice.Number}";
                    context.Request.Host = new HostString("localhost", 7257);
                    Console.WriteLine("Get invoice request detected.");
                }
                else if (context.Request.Method == HttpMethods.Post)
                {
                    context.Request.Path = "/api/invoices/all";
                    context.Request.Host = new HostString("localhost", 7257);
                    Console.WriteLine("Post invoices request detected.");
                }
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Invalid request model.");
                return;
            }

            await _next(context);
        }
    }
}
/**
 * 
 * using Gateway_Ocelot.Models;
using Newtonsoft.Json;
using System.Text;

namespace Gateway_Ocelot.Middlewares
{
    using System.IO;
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json.Linq;

    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Method == HttpMethods.Post && context.Request.Path.Value.Contains("/auth"))
            {
                context.Request.EnableBuffering();

                // Read the request body
                var body = await new StreamReader(context.Request.Body).ReadToEndAsync();
                context.Request.Body.Position = 0;

                // Parse the JSON body
                var payload = JObject.Parse(body);

                // Modify the request path based on the payload content
                if (payload.ContainsKey("email")) // Check for email to determine registration
                {
                    context.Request.Path = "/api/auth/register"; // Modify the path for registration
                    Console.WriteLine("Register request detected.");
                }
                else if (payload.ContainsKey("username")) // Or check for username for login
                {
                    context.Request.Path = "/api/auth/login"; // Modify the path for login
                    Console.WriteLine("Login request detected.");
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest; // Bad request for invalid payload
                    return;
                }
            }

            await _next(context);
        }
    }
}

 * 
 * **/