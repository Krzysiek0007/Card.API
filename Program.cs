
using Card.API.Services;
using Card.API.Strategies;

namespace Card.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddSingleton<ICardService, CardService>();
            builder.Services.AddScoped<ICardActionService, CardActionService>();

            // Register strategies  
            builder.Services.AddSingleton<ICardActionStrategy, PrepaidCardActionStrategy>();
            builder.Services.AddSingleton<ICardActionStrategy, CreditCardActionStrategy>();
            builder.Services.AddSingleton<ICardActionStrategy, DebitCardActionStrategy>();
            builder.Services.AddSingleton<ICardStatusActionStrategy, OrderedCardStatusActionStrategy>();
            builder.Services.AddSingleton<ICardStatusActionStrategy, InactiveCardStatusActionStrategy>();
            builder.Services.AddSingleton<ICardStatusActionStrategy, ActiveCardStatusActionStrategy>();
            builder.Services.AddSingleton<ICardStatusActionStrategy, RestrictedCardStatusActionStrategy>();
            builder.Services.AddSingleton<ICardStatusActionStrategy, BlockedCardStatusActionStrategy>();
            builder.Services.AddSingleton<ICardStatusActionStrategy, ExpiredCardStatusActionStrategy>();
            builder.Services.AddSingleton<ICardStatusActionStrategy, ClosedCardStatusActionStrategy>();
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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
