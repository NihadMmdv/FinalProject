using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using FinalProject.DAL.Data;
using FinalProject.Service.Services.Implementations;
using FinalProject.Service.Services.Interfaces;
using FinalProject.DAL.Repository.Interface;
using FinalProject.DAL.Repository;
using FinalProject.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.App;

public class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Load Configuration
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // Configure Serilog
        builder.Host.UseSerilog((context, loggerConfig) =>
        {
            loggerConfig
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.MSSqlServer(
                    connectionString: configuration.GetConnectionString("Default"),
                    sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
                    {
                        TableName = "Logs",
                        AutoCreateSqlTable = true
                    });
        });

        // Add Core Services
        builder.Services.AddDbContext<CustomDBContext>(opts =>
            opts.UseSqlServer(configuration.GetConnectionString("Default")));
        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddScoped<IAboutSkillService, AboutSkillService>();
        builder.Services.AddScoped<IAboutTextService, AboutTextService>();
        builder.Services.AddScoped<IAccountService, AccountService>();
        builder.Services.AddScoped<IAssociateService, AssociateService>();
        builder.Services.AddScoped<IAuctionService, AuctionService>();
        builder.Services.AddScoped<IBanService, BanService>();
        builder.Services.AddScoped<IBidService, BidService>();
        builder.Services.AddScoped<IBlogService, BlogService>();
        builder.Services.AddScoped<IBookService, BookService>();
        builder.Services.AddScoped<IBraintreeService, BraintreeService>();
        builder.Services.AddScoped<IBrandService, BrandService>();
        builder.Services.AddScoped<ICarImageService, CarImageService>();
        builder.Services.AddScoped<ICarService, CarService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IColorService, ColorService>();
        builder.Services.AddScoped<ICommentService, CommentService>();
        builder.Services.AddScoped<ICountryService, CountryService>();
        builder.Services.AddScoped<IEmailService, EmailService>();
        builder.Services.AddScoped<IFeatureService, FeatureService>();
        builder.Services.AddScoped<IFuelService, FuelService>();
        builder.Services.AddScoped<IMessageService, MessageService>();
        builder.Services.AddScoped<IModelService, ModelService>();
        builder.Services.AddScoped<IPositionService, PositionService>();
        builder.Services.AddScoped<ISettingService, SettingService>();
        builder.Services.AddScoped<ISliderService, SliderService>();
        builder.Services.AddScoped<ISocialService, SocialService>();
        builder.Services.AddScoped<IStaffService, StaffService>();
        builder.Services.AddScoped<IStatusService, StatusService>();
        builder.Services.AddScoped<ISubscribeService, SubscribeService>();
        builder.Services.AddScoped<ITagService, TagService>();
        builder.Services.AddScoped<ITextWhyService, TextWhyService>();
        builder.Services.AddScoped<ITelegramBotService, TelegramBotService>();
        builder.Services.AddScoped<IUserPricingService, UserPricingService>();
        builder.Services.AddIdentity<AppUser, IdentityRole>()
          .AddEntityFrameworkStores<CustomDBContext>()
            .AddDefaultTokenProviders();
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        });
		builder.Services.AddCors(options =>
		{
			options.AddPolicy("AllowAll",
				policy => policy.AllowAnyOrigin()
								.AllowAnyMethod()
								.AllowAnyHeader());
		});
		builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages(); 
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        // Add FluentValidation
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddFluentValidationClientsideAdapters();

        // Register Telegram Bot Service
        builder.Services.AddSingleton<ITelegramBotService, TelegramBotService>();

        var app = builder.Build();

        // Middleware Configuration
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
		app.UseCors("AllowAll"); // <-- Apply CORS
		app.UseSerilogRequestLogging();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();

        // Define Routes
        app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
            

        // Run Telegram Bot in the Background
        var telegramBotService = app.Services.GetRequiredService<ITelegramBotService>();
        _ = Task.Run(() => telegramBotService.StartBotAsync(configuration));

        // Start Application
        app.Run();
    }
}
