using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using BookStore.App.Interfaces;
using BookStore.App.Services;

namespace BookStore.App
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddHttpClient<ICategoryDataService, CategoryDataService>(client => client.BaseAddress = new Uri("https://localhost:44382/"));
            builder.Services.AddHttpClient<IBookDataService, BookDataService>(client => client.BaseAddress = new Uri("https://localhost:44382/"));

            await builder.Build().RunAsync();
        }
    }
}
