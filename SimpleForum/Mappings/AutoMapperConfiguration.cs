using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleForum.Mappings
{
    public static class AutoMapperConfiguration
    {
        public static void Initialize()
        {
            Mapper.Initialize(config =>
            {
                config.AddProfile<TopicProfile>();
                config.AddProfile<MessageProfile>();
            });
        }
    }
}