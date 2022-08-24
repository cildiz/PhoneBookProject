using Microsoft.EntityFrameworkCore;
using Report.API.Constants;
using Report.API.Contexts;
using Report.API.Middlewares;
using Report.API.ServiceExtensions;
using Report.API.Services;
using Report.API.Services.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ReportContext>(option => option.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection")));
builder.Services.Configure<ReportSettings>(builder.Configuration.GetSection("Options"));
builder.Services.AddScoped<IReportRepository, ReportService>();
builder.Services.AddHttpClient();

var app = builder.Build();
app.Services.CreateScope().ServiceProvider.GetRequiredService<ReportContext>().Database.Migrate();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseDeveloperExceptionPage();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "test v1"));
}

app.UseRabbitMq();

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


