using BlitzFlug;
using Microsoft.EntityFrameworkCore;
using BlitzFlug.Data;
using BlitzFlug.Models;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

builder.Services.AddDbContext<BlitzFlugContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

startup.Configure(app, app.Environment);
