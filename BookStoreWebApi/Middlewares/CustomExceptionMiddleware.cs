using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using Newtonsoft.Json;

namespace BookStoreWebApi.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            //requeste girip çıktığı süreyi hesaplamak için
            var watch = Stopwatch.StartNew();
            try
            {
                //request log
                string message = "[Request]  HTTP" + context.Request.Method + " - " + context.Request.Path;
                Console.WriteLine(message);

                //bi sonraki middleware i çağırmak 
                await _next(context);
                watch.Stop();

                //response log
                message = "[Response] HTTP " + context.Request.Method + " - " + context.Request.Path + " responded" + context.Response.StatusCode + " in" + watch.Elapsed.TotalMilliseconds + "ms";
                Console.WriteLine(message);
            }
            catch (System.Exception ex)
            {
                //hata alırsa _next() den sonra stop'u çalıştıramayacağı için burda stop methodunu çağırırız.

                watch.Stop();
                await HandleException(context, ex, watch);

            }




        }

        private Task HandleException(HttpContext context, Exception ex, Stopwatch watch)
        {
            //Hata mesajını anlamlı bir şekilde dönebilmek için context'e yazarız.
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);

            //hata durumunda geriye dönülecek ve log'a yazılacak mesaj
            string message = "[Error]   HTTP " + context.Request.Method + " - " + context.Response.StatusCode + " Error Message " + ex.Message + " in" + watch.Elapsed.TotalMilliseconds + " ms";

            Console.WriteLine(message);

            //json serileştirme için Newtonsoft paketini kullanırız
            var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);

            //geri dönmek için result'ı context içerisine yazarız
            return context.Response.WriteAsync(result);
        }
    }

    //program.js içerisinde bu middleware i app.use diye kullanabilmek için extension yazmamız gerekir.

    public static class CustomExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseCustomExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionMiddleware>();
        }
    }
}