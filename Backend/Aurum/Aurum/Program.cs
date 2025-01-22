using Aurum.Repositories.Income.Income;
using Aurum.Repositories.Income.IncomeCategory;
using Aurum.Repositories.Income.RegularIncome;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IIncomeRepo, IncomeRepo>();
builder.Services.AddScoped<IRegularIncomeRepo, RegularIncomeRepo>();
builder.Services.AddScoped<IIncomeCategoryRepo, IncomeCategoryRepo>();

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