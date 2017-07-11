using AutoMapper;
using SimpleForum.Logic.DTO.Topic;
using SimpleForum.Models.Topic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleForum.Mappings
{
    class TopicProfile : Profile
    {
        public TopicProfile()
        {
            this.CreateMap<TopicListDTO, TopicListModel>();
            this.CreateMap<TopicCreateModel, TopicCreateDTO>()
                .ForMember(dest => dest.CreatorLogin,
                opts => opts.MapFrom(src => HttpContext.Current.User.Identity.Name));
            this.CreateMap<TopicDetailsDTO, TopicDetailsModel>();
        }
    }
}