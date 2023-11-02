using assignment.Context;
using Microsoft.EntityFrameworkCore;


namespace assignment
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configure Entity Framework Core to use PostgreSQL
            services.AddDbContext<CarDBContext>(options =>
                options.UseNpgsql(_configuration.GetConnectionString("DefaultConnection")));

            // Other service configurations...

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app)
        {
            // Configure middleware, routing, etc.
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
