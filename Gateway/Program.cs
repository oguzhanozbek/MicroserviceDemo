using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Gateway;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Configuration.AddJsonFile("ocelot.json", optional:false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseMiddleware<Calculation>();
await app.UseOcelot();

app.Run();
