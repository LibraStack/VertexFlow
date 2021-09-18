﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VertexFlow.WebApplication.Interfaces;
using VertexFlow.WebApplication.Interfaces.Repositories;
using VertexFlow.WebInfrastructure.Repositories;

namespace VertexFlow.WebInfrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSignalR();
            services.AddSingleton<IMeshNotifier, MeshNotifier>();
            services.AddSingleton<IMeshRepository>(
                InitializeCosmosClientInstanceAsync(configuration.GetSection("CosmosDb")).GetAwaiter().GetResult());
        }
        
        public static void MapNotifiers(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapHub<MeshHub>("/notification");
        }

        private static async Task<MeshRepository> InitializeCosmosClientInstanceAsync(IConfiguration configurationSection)
        {
            var uri = configurationSection["Uri"];
            var key = configurationSection["Key"];
            var databaseName = configurationSection["DatabaseName"];
            var containerName = configurationSection["ContainerName"];

            var client = new CosmosClient(uri, key);
            var database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");
            
            return new MeshRepository(client.GetContainer(databaseName, containerName));
        }
    }
}