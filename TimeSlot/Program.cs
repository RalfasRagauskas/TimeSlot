using Microsoft.EntityFrameworkCore;
using TimeSlot.Data;
using TimeSlot.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<TimeSlotContext>(options => 
{ options.UseSqlServer
(builder.Configuration.GetConnectionString("TimeSlotConnection")); });


builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Bookings}/{action=Index}/{id?}");

app.Run();
