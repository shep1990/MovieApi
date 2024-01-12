using MovieApi.Data;
using MovieApi.Initialization;

namespace MovieApi
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<CosmosConfig>(builder.Configuration.GetSection("Cosmos"));
            return builder;
        }

        public static async Task<WebApplication> Initialize(this WebApplication app)
        {
            var initializer = app.Services.GetRequiredService<IApplicationInitializer>();

            await initializer.Initialize(app.Services, app.Environment);
            return app;
        }
    }
}
