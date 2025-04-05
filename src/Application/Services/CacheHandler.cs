using Application.Contracts;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Services;

public class CacheHandler(IMemoryCache cache) : ICacheHandler
{
}