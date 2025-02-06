using System.Text.Json;
using Aurum.Data.Context;
using Aurum.Data.Entities;
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowFrontEnd");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();