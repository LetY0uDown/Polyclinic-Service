using API_Host.Tools.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(useAuthorization: false);
builder.Services.AddRouteConstraints();
builder.Services.AddDatabase();
builder.Services.AddTools(builder.Configuration);

var app = builder.Build();

app.AddExceptionHandler(app.Services.GetService<ILogger<Program>>());

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();