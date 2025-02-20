using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FinalProject.DAL.Entities;
using FinalProject.DAL.Repository;
using FinalProject.DAL.Repository.Interface;
using FinalProject.DAL.Data;
using FinalProject.Service.Profiles.Categories;
using FinalProject.Service.Services.Implementations;
using FinalProject.Service.Services.Interfaces;
using FinalProject.Service.Validations.Categories;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Sinks.MSSqlServer;
using System.Text.Json.Serialization;

namespace FinalProject.App.ServiceRegistrations
{
    public static class ServiceRegister
    {
        public static void Register(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers()?.AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<CategoryPostDtoValidation>());
            services.AddDbContext<CustomDBContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("Default"));
            });
            services.AddIdentity<AppUser, IdentityRole>()
                    .AddDefaultTokenProviders()
                           .AddEntityFrameworkStores<CustomDBContext>();
            services.Configure<IdentityOptions>(options =>
            {
                // Default Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.AllowedForNewUsers = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.SignIn.RequireConfirmedEmail = true;
                options.User.RequireUniqueEmail = true;
            });
            services.AddAutoMapper(typeof(CategoryProfile));
            services.AddScoped<IGenericRepository<Category>, GenericRepository<Category>>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IGenericRepository<Tag>, GenericRepository<Tag>>();
            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IGenericRepository<Book>, GenericRepository<Book>>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IGenericRepository<Blog>, GenericRepository<Blog>>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IGenericRepository<Color>, GenericRepository<Color>>();
            services.AddScoped<IColorService, ColorService>();
            services.AddScoped<IGenericRepository<Message>, GenericRepository<Message>>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IGenericRepository<Feature>, GenericRepository<Feature>>();
            services.AddScoped<IFeatureService, FeatureService>();
            services.AddScoped<IGenericRepository<UserPricing>, GenericRepository<UserPricing>>();
            services.AddScoped<IUserPricingService, UserPricingService>();
            services.AddScoped<IGenericRepository<Associate>, GenericRepository<Associate>>();
            services.AddScoped<IAssociateService, AssociateService>();
            services.AddScoped<IGenericRepository<Slider>, GenericRepository<Slider>>();
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<IGenericRepository<Ban>, GenericRepository<Ban>>();
            services.AddScoped<IBanService, BanService>();
            services.AddScoped<IGenericRepository<Auction>, GenericRepository<Auction>>();
            services.AddScoped<IAuctionService, AuctionService>();
            services.AddScoped<IGenericRepository<Bid>, GenericRepository<Bid>>();
            services.AddScoped<IBidService, BidService>();
            services.AddScoped<IGenericRepository<Brand>, GenericRepository<Brand>>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IGenericRepository<Status>, GenericRepository<Status>>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IGenericRepository<Model>, GenericRepository<Model>>();
            services.AddScoped<IModelService, ModelService>();
            services.AddScoped<IGenericRepository<Subscribe>, GenericRepository<Subscribe>>();
            services.AddScoped<ISubscribeService, SubscribeService>();
            services.AddScoped<IGenericRepository<TextWhy>, GenericRepository<TextWhy>>();
            services.AddScoped<ITextWhyService, TextWhyService>();
            services.AddScoped<IGenericRepository<Fuel>, GenericRepository<Fuel>>();
            services.AddScoped<IFuelService, FuelService>();
            services.AddScoped<IGenericRepository<Car>, GenericRepository<Car>>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IGenericRepository<CarImage>, GenericRepository<CarImage>>();
            services.AddScoped<ICarImageService, CarImageService>();
            services.AddScoped<IGenericRepository<Country>, GenericRepository<Country>>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IGenericRepository<Setting>, GenericRepository<Setting>>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<IGenericRepository<Position>, GenericRepository<Position>>();
            services.AddScoped<IPositionService, PositionService>();
            services.AddScoped<IGenericRepository<Staff>, GenericRepository<Staff>>();
            services.AddScoped<IStaffService, StaffService>();
            services.AddScoped<IGenericRepository<Social>, GenericRepository<Social>>();
            services.AddScoped<ISocialService, SocialService>();
            services.AddScoped<IGenericRepository<AboutText>, GenericRepository<AboutText>>();
            services.AddScoped<IAboutTextService, AboutTextService>();
            services.AddScoped<IGenericRepository<AboutSkill>, GenericRepository<AboutSkill>>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IGenericRepository<Comment>, GenericRepository<Comment>>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAboutSkillService, AboutSkillService>();
            services.AddScoped<IGenericRepository<BlogCategory>, GenericRepository<BlogCategory>>();
            services.AddScoped<IGenericRepository<BlogTag>, GenericRepository<BlogTag>>();
            services.AddTransient<IBraintreeService, BraintreeService>();
            services.AddHttpContextAccessor();

            services.AddControllers().AddJsonOptions(x =>
                            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            services.AddSwaggerGen();
            services.AddFluentValidationRulesToSwagger();

            Log.Logger = new LoggerConfiguration()
               .WriteTo.MSSqlServer(
                   connectionString: configuration.GetSection("Serilog:ConnectionStrings:LogDatabase").Value,
                   tableName: configuration.GetSection("Serilog:TableName").Value,
                   appConfiguration: configuration,
                   autoCreateSqlTable: true,
                   columnOptionsSection: configuration.GetSection("Serilog:ColumnOptions"),
                   schemaName: configuration.GetSection("Serilog:SchemaName").Value)
                .WriteTo.Console(new JsonFormatter())
                .WriteTo.File("log.txt")
               .CreateLogger();
        }
    }
}
