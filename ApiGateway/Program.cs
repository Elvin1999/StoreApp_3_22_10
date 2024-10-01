using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);


IConfiguration  configuration=new ConfigurationBuilder()
    .AddJsonFile("ocelot.json")
    .Build();

builder.Services.AddOcelot(configuration);
// Add services to the container.

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.WebHost.UseUrls("https://*:22955");

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

app.UseCors("corsapp");
await app.UseOcelot();

app.Run();
