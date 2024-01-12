var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
