using AutoMapper;
using SimpleForum.Logic.DTO.Message;
using SimpleForum.Models.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleForum.Mappings
{
    class MessageProfile : Profile
    {
        public MessageProfile()
        {
            this.CreateMap<MessageCreateModel, MessageCreateDTO>()
                .ForMember(dest => dest.SenderLogin,
                opts => opts.MapFrom(src => HttpContext.Current.User.Identity.Name));
            this.CreateMap<MessageListDTO, MessageListModel>();
        }
    }
}