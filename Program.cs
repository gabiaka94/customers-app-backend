using CustomerStoreApi.Models;
using  CustomerStoreApi.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyPolicy",
                policy =>
                {
                    policy.WithOrigins(
                        "http://localhost:4200")
                           .AllowAnyMethod()
                           .AllowAnyOrigin()
                           .AllowAnyHeader();
                });
});


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




builder.Services.Configure<CustomerStoreDatabaseSettings>(
    builder.Configuration.GetSection("CustomerStoreDatabase"));

    builder.Services.AddSingleton<CustomersService>();
      builder.Services.AddSingleton<InvoicesService>();

builder.Services.AddControllers();
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();