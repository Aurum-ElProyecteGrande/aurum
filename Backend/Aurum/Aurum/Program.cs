using Aurum.Repositories.Income.Income;
using Aurum.Repositories.Income.IncomeCategory;
using Aurum.Repositories.Income.RegularIncome;
using Aurum.Models.CustomJsonConverter;
using Aurum.Models.RegularExpenseDto;
using Aurum.Models.RegularityEnum;
using Aurum.Services.AccountService;
using Aurum.Services.BallanceService;
using Aurum.Services.Income;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
	.AddJsonOptions(options => 
	{ 
		options.JsonSerializerOptions.Converters.Add(new CaseInsensitiveEnumConverter<Regularity>()); 
	});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IIncomeRepo, IncomeRepo>();
builder.Services.AddScoped<IRegularIncomeRepo, RegularIncomeRepo>();
builder.Services.AddScoped<IIncomeCategoryRepo, IncomeCategoryRepo>();
builder.Services.AddScoped<IIncomeService, IncomeService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IBalanceService, BalanceService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();