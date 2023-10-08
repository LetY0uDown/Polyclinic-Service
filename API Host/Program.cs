using API_Host.Tools.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(useAuthorization: true);
builder.Services.AddDatabase();
builder.Services.AddJWTAuthentication(builder.Configuration);
builder.Services.AddTools();

// ’з, вроде не нужно, но лучше не трогать
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
        builder.WithOrigins("http://localhost:44489")
               .AllowAnyMethod()
               .AllowAnyHeader()
    );
});

var app = builder.Build();

app.AddExceptionHandler(app.Services.GetService<ILogger<Program>>()!);

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseAuthorization();
app.MapControllers();

app.Run();