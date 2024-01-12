using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using MovieApi.Data;

namespace MovieApi.Initialization
{
    public class MovieApiInitializer : IApplicationInitializer
    {
        public async Task Initialize(IServiceProvider provider, IWebHostEnvironment environment)
        {
            using var scope = provider.CreateScope();
            var cosmosConfig = scope.ServiceProvider.GetRequiredService<IOptions<CosmosConfig>>().Value;
            var cosmosClient = scope.ServiceProvider.GetRequiredService<CosmosClient>();

            await cosmosClient.CreateDatabaseIfNotExistsAsync(cosmosConfig.Database);
            await cosmosClient.GetDatabase(cosmosConfig.Database).CreateContainerIfNotExistsAsync(cosmosConfig.MovieContainer, cosmosConfig.MoviePartition);
        }
    }
}
