using System.Globalization;
using System.Text;
using System.Text.Json;
using Aurum.Data.Context;
using Aurum.Data.Entities;
using Aurum.Data.Seeders;
using Aurum.Repositories.IncomeRepository.RegularIncomeRepository;
using Aurum.Models.CustomJsonConverter;
using Aurum.Models.RegularExpenseDto;
using Aurum.Models.RegularityEnum;
using Aurum.Repositories.AccountRepo;
using Aurum.Repositories.AccountRepository;
using Aurum.Repositories.CurrencyRepository;
using Aurum.Repositories.ExpenseCategoryRepository;
using Aurum.Repositories.ExpenseRepository;
using Aurum.Repositories.IncomeRepository.IncomeCategoryRepository;
using Aurum.Repositories.IncomeRepository.IncomeRepository;
using Aurum.Repositories.IncomeRepository.RegularIncomeRepository;
using Aurum.Repositories.RegularExpenseRepository;
using Aurum.Services.AccountService;
using Aurum.Services.BalanceService;
using Aurum.Services.ExpenseCategoryService;
using Aurum.Services.ExpenseService;
using Aurum.Services.IncomeServices;
using Aurum.Services.RegularExpenseService;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting.Server;
using Aurum.Services.RegularIncomeServices;
using Aurum.Services.IncomeCategoryServices;
using Aurum.Services.UserServices;
using Aurum.Repositories.UserRepository;
using Aurum.Services.CurrencyServices;
using Aurum.Repositories.LayoutRepository;
using Aurum.Services.LayoutServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new CaseInsensitiveEnumConverter<Regularity>());
        options.JsonSerializerOptions.Converters.Add(new CategoryDictionaryConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

AddDatabase(builder);
AddAuthentication(builder);
AddIdentity(builder);
AddCookiePolicy(builder);
AddServices(builder);
AddCors(builder);

var app = builder.Build();

using var scope = app.Services.CreateScope();
var authenticationSeeder = scope.ServiceProvider.GetRequiredService<AuthenticationSeeder>();
authenticationSeeder.AddRoles();
authenticationSeeder.AddAdmin();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowFrontEnd");

app.UseCookiePolicy();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

void AddServices(WebApplicationBuilder webApplicationBuilder)
{
    webApplicationBuilder.Services.AddScoped<ITokenService, TokenService>();
    webApplicationBuilder.Services.AddScoped<IIncomeRepo, IncomeRepo>();
    webApplicationBuilder.Services.AddScoped<IAccountRepo, AccountRepo>();
    webApplicationBuilder.Services.AddScoped<ICurrencyRepo, CurrencyRepo>();
    webApplicationBuilder.Services.AddScoped<IRegularIncomeRepo, RegularIncomeRepo>();
    webApplicationBuilder.Services.AddScoped<IIncomeCategoryRepo, IncomeCategoryRepo>();
    webApplicationBuilder.Services.AddScoped<IExpenseCategoryRepository, ExpenseCategoryRepository>();
    webApplicationBuilder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
    webApplicationBuilder.Services.AddScoped<IRegularExpenseRepository, RegularExpenseRepository>();
    webApplicationBuilder.Services.AddScoped<IIncomeService, IncomeService>();
    webApplicationBuilder.Services.AddScoped<IAccountService, AccountService>();
    webApplicationBuilder.Services.AddScoped<IExpenseCategoryService, ExpenseCategoryService>();
    webApplicationBuilder.Services.AddScoped<IExpenseService, ExpenseService>();
    webApplicationBuilder.Services.AddScoped<IRegularExpenseService, RegularExpenseService>();
    webApplicationBuilder.Services.AddScoped<IBalanceService, BalanceService>();
    webApplicationBuilder.Services.AddScoped<IRegularIncomeService, RegularIncomeService>();
    webApplicationBuilder.Services.AddScoped<IIncomeCategoryService, IncomeCategoryService>();
    webApplicationBuilder.Services.AddScoped<IUserService, UserService>();
    webApplicationBuilder.Services.AddScoped<IUserRepo, UserRepo>();
    webApplicationBuilder.Services.AddScoped<ICurrencyService, CurrencyService>();
    webApplicationBuilder.Services.AddScoped<ILayoutRepo, LayoutRepo>();
    webApplicationBuilder.Services.AddScoped<ILayoutService, LayoutService>();
    webApplicationBuilder.Services.AddScoped<AuthenticationSeeder>();
}

void AddCors(WebApplicationBuilder builder1)
{
    builder1.Services.AddCors(options =>
    {
        options.AddPolicy("AllowFrontEnd",
            policy =>
            {
                policy.WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
    });
}

void AddIdentity(WebApplicationBuilder webApplicationBuilder1)
{
    webApplicationBuilder1.Services
        .AddIdentityCore<IdentityUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.User.RequireUniqueEmail = true;
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<AurumContext>();
}

void AddAuthentication(WebApplicationBuilder builder2)
{
    builder2.Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ClockSkew = TimeSpan.Zero,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "apiWithAuthBackend",
                ValidAudience = "apiWithAuthBackend",
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes("!SomethingSecret!!SomethingSecret!")
                ),
            };
        });
}

void AddDatabase(WebApplicationBuilder webApplicationBuilder2)
{
    webApplicationBuilder2.Services.AddDbContext<AurumContext>(options =>
    {
        options.UseSqlServer(
             // "Server=db;Database=Aurum;User Id=sa;Password=yourStrong(!)Password;Encrypt=false;",
             Environment.GetEnvironmentVariable("DbConnectionString"),
            sqlOptions => sqlOptions.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(10),
                errorNumbersToAdd: null
            ));
    });
}

void AddCookiePolicy(WebApplicationBuilder builder3)
{
    builder3.Services.Configure<CookiePolicyOptions>(options =>
    {
        options.HttpOnly = HttpOnlyPolicy.Always;   
        options.Secure = CookieSecurePolicy.Always;
        options.MinimumSameSitePolicy = SameSiteMode.None;
    });
}