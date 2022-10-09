using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Pit.Api.Services;
using Pit.Api.Services.Interfaces;
using Pit.Database;
using Pit.Database.Migrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PitContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("database19")));
builder.Services.AddScoped<IProdutoService, ProdutoService>();

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

app.Run();

static void UpdateDatabase(IServiceProvider serviceProvider)
{
    // Instantiate the runner
    var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

    // Execute the migrations
    runner.MigrateUp();
}

static IServiceProvider CreateServices(WebApplicationBuilder builder)
{
    return new ServiceCollection()
        .AddFluentMigratorCore()
    .ConfigureRunner(rb => rb.AddPostgres()
    .WithGlobalConnectionString(builder.Configuration.GetConnectionString("databse19"))
    .ScanIn(typeof(TabelaProduto).Assembly).For.Migrations())
    .AddLogging(lb => lb.AddFluentMigratorConsole())
    .BuildServiceProvider(false);
}