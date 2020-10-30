using IdentityServer4Management.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer4Management.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //proje ayaga kalkarken eger haz�rda bekleyen migration varsa yani veritaban�na aktar�lmam�� ama bekleyen bir migration varsa veritaban�na Update-Database i�lemi yapar
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                SeedDatabase.EnsureSeedData(scope.ServiceProvider);
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    //webBuilder.UseUrls("http://0.0.0.0:5001"); //sabit port verdim uygulama ayaga kalk�nca ya localhost:5001 yada 127.0.0.1:5001 diye kalkacak
                });
    }
}
