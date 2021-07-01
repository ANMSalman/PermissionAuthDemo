using BlazorHero.CleanArchitecture.Client.Extensions;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Threading.Tasks;

namespace PermissionAuthDemo.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.AddClientServices();
            builder.Services.AddManagers();

            await builder.Build().RunAsync();
        }
    }
}
