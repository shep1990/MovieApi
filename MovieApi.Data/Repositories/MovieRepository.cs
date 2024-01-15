﻿using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using MovieApi.Data.Interfaces;
using MovieApi.Data.Models;
using System.Drawing.Printing;

namespace MovieApi.Data.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly CosmosClient _cosmosClient;
        private Database _database;
        private Container _container;
        const string databaseId = "movie-database";
        const string containerId = "movie-container";

        public MovieRepository(CosmosClient cosmosClient)
        {
            _cosmosClient = cosmosClient;
            _database = _cosmosClient.GetDatabase(databaseId);
            _container = _database.GetContainer(containerId);
        }

        public async Task<List<Movie>> Get()
        {
            var itemResponseFeed = _container.GetItemLinqQueryable<Movie>().ToFeedIterator();
            FeedResponse<Movie> queryResultSet = await itemResponseFeed.ReadNextAsync();
            return queryResultSet.ToList();
        }

        public async Task<List<Movie>> GetByPageNumber(int page, int pagesize = 10)
        {
            var itemResponseFeed = _container.GetItemLinqQueryable<Movie>().Skip(page * pagesize).Take(pagesize).ToFeedIterator();
            FeedResponse<Movie> queryResultSet = await itemResponseFeed.ReadNextAsync();
            return queryResultSet.ToList();
        }
    }
}
