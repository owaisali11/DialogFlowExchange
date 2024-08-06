using CustomExchangeEndpointProxy.Client;
using CustomExchangeEndpointProxy.Interface;
using CustomExchangeEndpointProxy.Manager;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Dialogflow Webhook API", Version = "v1" });
});

builder.Services.AddScoped<ICustomExchangeManager, CustomExchangeManager>();
builder.Services.AddScoped<ICustomExchangeEndpointService, CustomExchangeEndpointService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
// Configure the HTTP request pipeline.
app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Dialogflow Webhook API v1"));


app.UseHttpsRedirection();

app.UseAuthorization();
app.UseRouting();
//app.MapControllers();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.Run();