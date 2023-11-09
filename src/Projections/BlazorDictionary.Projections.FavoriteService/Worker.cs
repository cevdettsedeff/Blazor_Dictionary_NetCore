using BlazorDictionary.Common;
using BlazorDictionary.Common.Events.Entry;
using BlazorDictionary.Common.Events.EntryComment;
using BlazorDictionary.Common.Infrastructure;

namespace BlazorDictionary.Projections.FavoriteService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var connStr = _configuration.GetConnectionString("SqlConnection");

            var favService = new Services.FavoriteService(connStr);

            QueueFactory.CreateBasicConsumer()
                .EnsureExchange(DictionaryConstants.FavExchangeName)
                .EnsureQueue(DictionaryConstants.CreateEntryFavQueueName, DictionaryConstants.FavExchangeName)
                .Receive<CreateEntryFavEvent>(fav =>
                {
                    favService.CreateEntryFav(fav).GetAwaiter().GetResult();
                    _logger.LogInformation($"Received EntryId {fav.EntryId}");
                })
                .StartConsuming(DictionaryConstants.CreateEntryFavQueueName);

            QueueFactory.CreateBasicConsumer()
                .EnsureExchange(DictionaryConstants.FavExchangeName)
                .EnsureQueue(DictionaryConstants.DeleteEntryFavQueueName, DictionaryConstants.FavExchangeName)
                .Receive<DeleteEntryFavEvent>(fav =>
                {
                    favService.DeleteEntryFav(fav).GetAwaiter().GetResult();
                    _logger.LogInformation($"Deleted Received EntryId {fav.EntryId}");
                })
                .StartConsuming(DictionaryConstants.DeleteEntryFavQueueName);

            QueueFactory.CreateBasicConsumer()
                .EnsureExchange(DictionaryConstants.FavExchangeName)
                .EnsureQueue(DictionaryConstants.CreateEntryCommentFavQueueName, DictionaryConstants.FavExchangeName)
                .Receive<CreateEntryCommentFavEvent>(fav =>
                {
                    favService.CreateEntryCommentFav(fav).GetAwaiter().GetResult();
                    _logger.LogInformation($"Create EntryComment Received EntryCommentId {fav.EntryCommentId}");
                })
                .StartConsuming(DictionaryConstants.CreateEntryCommentFavQueueName);


            QueueFactory.CreateBasicConsumer()
                .EnsureExchange(DictionaryConstants.FavExchangeName)
                .EnsureQueue(DictionaryConstants.DeleteEntryCommentFavQueueName, DictionaryConstants.FavExchangeName)
                .Receive<DeleteEntryCommentFavEvent>(fav =>
                {
                    favService.DeleteEntryCommentFav(fav).GetAwaiter().GetResult();
                    _logger.LogInformation($"Deleted Received EntryCommentId {fav.EntryCommentId}");
                })
                .StartConsuming(DictionaryConstants.DeleteEntryCommentFavQueueName);
        }
    }
}