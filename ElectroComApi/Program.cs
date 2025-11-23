using Microsoft.EntityFrameworkCore;
using ElectroComApi.Data;
using ElectroComApi.Repo;
using ElectroComApi.Services;
using AutoMapper;
using ElectroComApi.Profiles;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IQuotationRequestRepo, QuotationRequestRepo>();
builder.Services.AddScoped<IQuotationResponseRepo, QuotationResponseRepo>();
builder.Services.AddScoped<IProductQuoteInfoRepo, ProductQuoteInfoRepo>();
builder.Services.AddScoped<IOrderConfirmationRepo, OrderConfirmationRepo>();
builder.Services.AddScoped<IQuotationService, QuotationService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IDistributorMappingService, DistributorMappingService>();
builder.Services.AddScoped<IGadgetHubNotificationService, GadgetHubNotificationService>();

// Add HttpClient for calling GadgetHub API
builder.Services.AddHttpClient<IDistributorMappingService, DistributorMappingService>();
builder.Services.AddHttpClient<IGadgetHubNotificationService, GadgetHubNotificationService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.CustomSchemaIds(type => type.FullName);
});
builder.Services.AddAutoMapper(typeof(OrderConfirmationProfile).Assembly);

var app = builder.Build();

// Seed the database
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDbContext>();
    ApplicationDbSeeder.Seed(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(policy =>
    policy.WithOrigins("http://localhost:4028")
          .AllowAnyHeader()
          .AllowAnyMethod()
);
app.UseAuthorization();
app.MapControllers();

app.Run();
