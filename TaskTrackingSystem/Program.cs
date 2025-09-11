using System.Reflection;
using System.Text.Json.Serialization;
using TaskTrackingSystem.Application.Common.Mapping;
using TaskTrackingSystem.Application.Users.Commands.Create;
using TaskTrackingSystem.Persistence;

namespace TaskTrackingSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddPersistence(builder.Configuration);

            builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

            builder.Services.AddMediatR(
                cfg => cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly));

            builder.Services.AddControllers().
                AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });
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
        }
    }
}
