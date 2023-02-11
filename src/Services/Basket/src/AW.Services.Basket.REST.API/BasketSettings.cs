namespace AW.Services.Basket.REST.API
{
    public class BasketSettings
    {
        public string ElasticSearchUri { get; set; }
        public string RedisConnectionString { get; set; }

        public BasketSettings(string elasticSearchUri, string redisConnectionString)
        {
            ElasticSearchUri = elasticSearchUri;
            RedisConnectionString = redisConnectionString;
        }
    }
}