using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace WebDemo.Utils
{
    public class RedisUtil
    {
        private readonly IDistributedCache Cache;

        public RedisUtil(IDistributedCache cache)
        {
            Cache = cache;
        }
        /// <summary>
        /// 从缓存中获取一个对象
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="key"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<TResult?> GetOne<TResult>(string key, CancellationToken cancellationToken = default)
        {
            string? json = await Cache.GetStringAsync(key, cancellationToken);
            TResult? res = default;
            if (json is null) { return res; }
            res = JsonConvert.DeserializeObject<TResult>(json, new JsonSerializerSettings
            {
                Converters = { new ActionConverter() }
            });
            return res;
        }

        public async Task<List<TResult>?> GetList<TResult>(string key, CancellationToken cancellationToken = default)
        {
            string? json = await Cache.GetStringAsync(key, cancellationToken);
            List<TResult>? resultList = default;
            if (json is null) { return resultList; }
            resultList = JsonConvert.DeserializeObject<List<TResult>>(json, new JsonSerializerSettings
            {
                Converters = { new ActionConverter() }
            });
            return resultList;
        }

        private class ActionConverter : JsonConverter<Action>
        {
            public override bool CanRead => false;

            public override void WriteJson(JsonWriter writer, Action? value, JsonSerializer serializer)
            {
                throw new NotSupportedException("Serializing 'System.Action' instances is not supported.");
            }

            public override Action ReadJson(JsonReader reader, Type objectType, Action? existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                throw new NotSupportedException("Deserializing 'System.Action' instances is not supported.");
            }
        }

        public async Task<bool> SetOne<T>(string key, T value, int second)
        {
            string json = JsonConvert.SerializeObject(value, new JsonSerializerSettings
            {
                ContractResolver = new IgnoreActionContractResolver()
            });
            await Cache.SetStringAsync(key, json, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(second),
                SlidingExpiration = TimeSpan.FromSeconds(10)
                
            });
            return true;
        }

        public async Task<bool> SetList<T>(string key, List<T> values, int second)
        {
            string json = JsonConvert.SerializeObject(values, new JsonSerializerSettings
            {
                ContractResolver = new IgnoreActionContractResolver()
            });
            await Cache.SetStringAsync(key, json, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(second),
                SlidingExpiration = TimeSpan.FromSeconds(10)
            });
            return true;
        }
        private class IgnoreActionContractResolver : DefaultContractResolver
        {
            protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
            {
                var property = base.CreateProperty(member, memberSerialization);
                if (property.PropertyType == typeof(Action))
                {
                    property.ShouldSerialize = _ => false;
                }
                return property;
            }
        }
        public async Task<TResult?> GetOrCreateAsync<TResult>(string key, Func<Task<TResult>> createFunc, int second, CancellationToken cancellationToken = default)
        {
            string? json = await Cache.GetStringAsync(key, cancellationToken);
            if (json is null)
            { 
                TResult result = await createFunc();
                await SetOne(key, result, second);
                return result;
            }
            return JsonConvert.DeserializeObject<TResult>(json);
        }

    }
}
