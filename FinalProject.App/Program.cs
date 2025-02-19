using FinalProject.DAL.Data;
using FinalProject.Service.Services.Telegram;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.App
{
	public class Program
	{
		static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			var configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
				.Build();

			// Add services to the container.
			builder.Services.AddControllersWithViews();

			builder.Services.AddDbContext<CustomDBContext>(
				opts => opts.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			Task.Run(() => TelegramBot.StartBotAsync(configuration));

			app.Run();
		}
	}
}
