using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleForum.Mappings
{
    static class AutoMapperExtensions
    {
        public static IEnumerable<TDestination> MapAll<TSource, TDestination>(this IMapper mapper, IEnumerable<TSource> source)
        {
            return source.Select(src => mapper.Map<TSource, TDestination>(src));
        }
    }
}