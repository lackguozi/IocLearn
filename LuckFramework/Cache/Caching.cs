using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckFramework.Cache
{
    public class Caching : ICaching
    {
        public Caching(IDistributedCache Cache)
        {
            _cache = Cache;
        }
        private readonly IDistributedCache _cache;
        public IDistributedCache Cache => _cache;

        public void AddCacheKey(string cacheKey)
        {
            throw new NotImplementedException();
        }

        public async Task AddCacheKeyAsync(string cacheKey)
        {
            var res = await _cache.GetStringAsync(CacheConst.KeyAll);
            var allkeys = string.IsNullOrWhiteSpace(res) ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(res);
            if (!allkeys.Any(m => m == cacheKey))
            {
                allkeys.Add(cacheKey);
                await _cache.SetStringAsync(CacheConst.KeyAll, JsonConvert.SerializeObject(allkeys));
            }
        }

        public Task DelByParentKeyAsync(string key)
        {
            throw new NotImplementedException();
        }

        public void DelByPattern(string key)
        {
            throw new NotImplementedException();
        }

        public Task DelByPatternAsync(string key)
        {
            throw new NotImplementedException();
        }

        public void DelCacheKey(string cacheKey)
        {
            var res = _cache.GetString(CacheConst.KeyAll);
            var allkeys = string.IsNullOrWhiteSpace(res) ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(res);
            if (allkeys.Any(m => m == cacheKey))
            {
                allkeys.Remove(cacheKey);
                _cache.SetString(CacheConst.KeyAll, JsonConvert.SerializeObject(allkeys));
            }
        }

        public async Task DelCacheKeyAsync(string cacheKey)
        {
            var res = await _cache.GetStringAsync(CacheConst.KeyAll);
            var allkeys = string.IsNullOrWhiteSpace(res) ? new List<string>() : JsonConvert.DeserializeObject<List<string>>(res);
            if (allkeys.Any(m => m == cacheKey))
            {
                allkeys.Remove(cacheKey);
                await _cache.SetStringAsync(CacheConst.KeyAll, JsonConvert.SerializeObject(allkeys));
            }
        }

        public bool Exists(string cacheKey)
        {
            var res = _cache.Get(cacheKey);
            return res != null;
        }

        public async Task<bool> ExistsAsync(string cacheKey)
        {
            var res = await _cache.GetAsync(cacheKey);
            return res != null;
        }

        public T Get<T>(string cacheKey)
        {
            var res = _cache.Get(cacheKey);
            return res == null ? default : JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(res));
        }

        public object Get(Type type, string cacheKey)
        {
            var res = _cache.Get(cacheKey);
            return res == null ? default : JsonConvert.DeserializeObject(Encoding.UTF8.GetString(res), type);
        }

        public List<string> GetAllCacheKeys()
        {
            var res = _cache.GetString(CacheConst.KeyAll);
            return string.IsNullOrWhiteSpace(res) ? null : JsonConvert.DeserializeObject<List<string>>(res);
        }

        public async Task<List<string>> GetAllCacheKeysAsync()
        {
            var res = await _cache.GetStringAsync(CacheConst.KeyAll);
            return string.IsNullOrWhiteSpace(res) ? null : JsonConvert.DeserializeObject<List<string>>(res);
        }

        public async Task<T> GetAsync<T>(string cacheKey)
        {
            var res = await _cache.GetAsync(cacheKey);
            return res == null ? default : JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(res));
        }

        public async Task<object> GetAsync(Type type, string cacheKey)
        {
            var res = await _cache.GetAsync(cacheKey);
            return res == null ? default : JsonConvert.DeserializeObject(Encoding.UTF8.GetString(res), type);
        }

        public string GetString(string cacheKey)
        {
            return _cache.GetString(cacheKey);
        }

        public async Task<string> GetStringAsync(string cacheKey)
        {
            return await _cache.GetStringAsync(cacheKey);
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void RemoveAll()
        {
            var catches = GetAllCacheKeys();
            foreach (var @catch in catches) Remove(@catch);

            catches.Clear();
            _cache.SetString(CacheConst.KeyAll, JsonConvert.SerializeObject(catches));
        }

        public async Task RemoveAllAsync()
        {
            var catches = await GetAllCacheKeysAsync();
            foreach (var @catch in catches) await RemoveAsync(@catch);

            catches.Clear();
            await _cache.SetStringAsync(CacheConst.KeyAll, JsonConvert.SerializeObject(catches));
        }

        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
            await DelCacheKeyAsync(key);
        }

        public void Set<T>(string cacheKey, T value, TimeSpan? expire = null)
        {
            _cache.Set(cacheKey, GetBytes(value),
            expire == null
                ? new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6) }
                : new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = expire });

            AddCacheKey(cacheKey);
        }
        private byte[] GetBytes<T>(T source)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(source));
        }

        public async Task SetAsync<T>(string cacheKey, T value)
        {
            await _cache.SetAsync(cacheKey, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)),
             new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6) });

            await AddCacheKeyAsync(cacheKey);
        }

        public async Task SetAsync<T>(string cacheKey, T value, TimeSpan expire)
        {
            await _cache.SetAsync(cacheKey, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(value)),
            new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = expire });

            await AddCacheKeyAsync(cacheKey);
        }

        public void SetPermanent<T>(string cacheKey, T value)
        {
            throw new NotImplementedException();
        }

        public Task SetPermanentAsync<T>(string cacheKey, T value)
        {
            throw new NotImplementedException();
        }

        public void SetString(string cacheKey, string value, TimeSpan? expire = null)
        {
            if (expire == null)
                _cache.SetString(cacheKey, value, new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6) });
            else
                _cache.SetString(cacheKey, value, new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = expire });

            AddCacheKey(cacheKey);
        }

        public async Task SetStringAsync(string cacheKey, string value)
        {
            await _cache.SetStringAsync(cacheKey, value, new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6) });

            await AddCacheKeyAsync(cacheKey);
        }

        public async Task SetStringAsync(string cacheKey, string value, TimeSpan expire)
        {
            await _cache.SetStringAsync(cacheKey, value, new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = expire });

            await AddCacheKeyAsync(cacheKey);
        }
    }
}
