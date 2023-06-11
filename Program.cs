using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MeetUp.Data;
using MeetUp.Repositories;
using MeetUp.Interfaces;
using MeetUp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MeetUpContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MeetUpContext") ?? throw new InvalidOperationException("Connection string 'MeetUpContext' not found.")));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IMeetActivityRepository, MeetActivityRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IUserActivityRepository, UserActivityRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<IMeetActivityService, MeetActivityService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUserActivityService, UserActivityService>();


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
