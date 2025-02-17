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

builder.Services.AddDbContext<AurumContext>(options =>
{
    options.UseSqlServer(
   Environment.GetEnvironmentVariable("DbConnectionString"),
sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorNumbersToAdd: null
        ));
});

builder.Services
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
builder.Services
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

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IIncomeRepo, IncomeRepo>();
builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddScoped<ICurrencyRepo, CurrencyRepo>();
builder.Services.AddScoped<IRegularIncomeRepo, RegularIncomeRepo>();
builder.Services.AddScoped<IIncomeCategoryRepo, IncomeCategoryRepo>();
builder.Services.AddScoped<IExpenseCategoryRepository, ExpenseCategoryRepository>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepository>();
builder.Services.AddScoped<IRegularExpenseRepository, RegularExpenseRepository>();
builder.Services.AddScoped<IIncomeService, IncomeService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IExpenseCategoryService, ExpenseCategoryService>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<IRegularExpenseService, RegularExpenseService>();
builder.Services.AddScoped<IBalanceService, BalanceService>();
builder.Services.AddScoped<IRegularIncomeService, RegularIncomeService>();
builder.Services.AddScoped<IIncomeCategoryService, IncomeCategoryService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddScoped<ILayoutRepo, LayoutRepo>();
builder.Services.AddScoped<ILayoutService, LayoutService>();
builder.Services.AddScoped<AuthenticationSeeder>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontEnd",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

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

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();