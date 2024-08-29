using Microsoft.AspNetCore.Builder;
using SmartTaskApp.CommonLib.Middlewares;

namespace SmartTaskApp.CommonLib.Configurations
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSmartTaskAppPipeline(this IApplicationBuilder app)
        {
            app.UseMiddleware<SmartTaskAppExceptionHandlingMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI();

            //TODO: Disable  just for  docker container
            // app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            return app;
        }
    }
}
