using EasyMicroservices.Cores.AspEntityFrameworkCoreApi;
using EasyMicroservices.Cores.Relational.EntityFrameworkCore.Intrerfaces;
using EasyMicroservices.EmailsMicroservice.Database.Contexts;

namespace EasyMicroservices.EmailsMicroservice.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var app = CreateBuilder(args);
            var build = await app.Build<EmailContext>(true);
            build.MapControllers();
            await build.RunAsync();
        }

        static WebApplicationBuilder CreateBuilder(string[] args)
        {
            var app = StartUpExtensions.Create<EmailContext>(args);
            app.Services.Builder<EmailContext>().UseDefaultSwaggerOptions();
            app.Services.AddTransient(serviceProvider => new EmailContext(serviceProvider.GetService<IEntityFrameworkCoreDatabaseBuilder>()));
            app.Services.AddTransient<IEntityFrameworkCoreDatabaseBuilder, DatabaseBuilder>();
            StartUpExtensions.AddWhiteLabel("Email", "RootAddresses:WhiteLabel");
            return app;
        }
    }
}