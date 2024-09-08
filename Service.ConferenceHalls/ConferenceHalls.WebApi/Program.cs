using ConferenceHalls.Infrastructure.Persistence;
using ConferenceHalls.WebApi.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();

builder.Services.AddMediatRConfigurationService();
builder.Services.AddDatabaseService(builder.Configuration);
builder.Services.AddMassTransitService(builder.Configuration);
builder.Services.AddAutoMapperService();
builder.Services.AddRepositories();
builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DatabaseInitializer.Initialize(services);
}

if (app.Environment.IsDevelopment())
{   
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

app.Run();
