var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddJsonOptions( option =>
    {

        option.JsonSerializerOptions.WriteIndented = true;

    });

builder.Services.AddSwaggerGen();


Application.Startup.ApplicationInit(builder.Services, builder.Configuration);
Infrastructure.Startup.InfrastructureInit(builder.Services);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseSwagger();
app.UseSwaggerUI( options =>
{
    options.RoutePrefix = "api/swagger";
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Parser");

});


app.UseAuthorization();

app.MapControllers();

app.Run();
