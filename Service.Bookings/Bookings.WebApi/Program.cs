using Bookings.WebApi.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddMediatRConfigurationService();
builder.Services.AddMongoDb(builder.Configuration);
builder.Services.AddMassTransitService(builder.Configuration);
builder.Services.AddAutoMapperService();
builder.Services.AddApplicationService();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
