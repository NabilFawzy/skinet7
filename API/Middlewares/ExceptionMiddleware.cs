using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;

namespace API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        public IHostEnvironment _Host { get; }

        public ExceptionMiddleware(RequestDelegate next,ILogger<ExceptionMiddleware>logger,IHostEnvironment host)
        {
            _Host = host;
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context){
                 try{
                     await _next(context);
                 }
                 catch(Exception ex){
                     _logger.LogError(ex,ex.Message);
                     context.Response.ContentType="application/json";
                     context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;


                     var response= _Host.IsDevelopment()?
                     new ApiException((int)HttpStatusCode.InternalServerError,ex.Message,ex.StackTrace.ToString()):
                     new ApiException((int)HttpStatusCode.InternalServerError);
                    
                    var optons=new JsonSerializerOptions{PropertyNamingPolicy=JsonNamingPolicy.CamelCase};
                     var json=JsonSerializer.Serialize(response,optons);

                     await context.Response.WriteAsJsonAsync(json);

                 }
        }
    }
}