using SimpleForum.Core.Entities;
using SimpleForum.Data.Contracts;
using SimpleForum.Logic.Contracts;
using SimpleForum.Logic.DTO.Message;
using SimpleForum.Logic.DTO.Topic;
using SimpleForum.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Logic.Services
{
    public class TopicService : ITopicService
    {
        private readonly IUnitOfWork unitOfWork;

        public TopicService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ServiceMessage Create(TopicCreateDTO topicDTO)
        {
            List<string> errors = new List<string>();
            bool succeeded = Validate(topicDTO, errors);

            if (succeeded)
            {
                try
                {
                    UserEntity userEntity = unitOfWork.Users.Get(topicDTO.CreatorLogin);
                    TopicEntity topicEntity = new TopicEntity
                    {
                        Creator = userEntity,
                        DateCreated = DateTime.Now,
                        DateOfLastMessage = DateTime.Now,
                        Title = topicDTO.Title,
                        Description = topicDTO.Description
                    };

                    unitOfWork.Topics.Add(topicEntity);
                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    succeeded = false;
                    ExceptionMessageBuilder.FillErrors(ex, errors);
                }
            }

            return new ServiceMessage
            {
                Errors = errors,
                Succeeded = succeeded
            };
        }

        public DataServiceMessage<TopicDetailsDTO> Get(int id)
        {
            List<string> errors = new List<string>();
            bool succeeded = true;
            TopicDetailsDTO topicDTO = null;

            try
            {
                TopicEntity topicEntity = unitOfWork.Topics.Get(id);
                if (topicEntity != null)
                {
                    topicDTO = new TopicDetailsDTO
                    {
                        Id = id,
                        CreatorLogin = topicEntity.Creator.ApplicationUser.UserName,
                        DateCreated = topicEntity.DateCreated,
                        Description = topicEntity.Description,
                        Title = topicEntity.Title,
                        Messages = topicEntity.Messages.Select(messageEntity =>
                        new MessageListDTO
                        {
                            Id = messageEntity.Id,
                            DateCreated = messageEntity.DateCreated,
                            DateLastModified = messageEntity.DateLastModified,
                            Text = messageEntity.Text,
                            SenderLogin = messageEntity.Sender.ApplicationUser.UserName
                        })
                        .OrderByDescending(message => message.DateCreated)
                        .ToList()
                    };
                }
                else
                {
                    succeeded = false;
                    errors.Add("Topic was not found");
                }
            }
            catch (Exception ex)
            {
                succeeded = false;
                ExceptionMessageBuilder.FillErrors(ex, errors);
            }

            return new DataServiceMessage<TopicDetailsDTO>
            {
                Errors = errors,
                Succeeded = succeeded,
                Data = topicDTO
            };
        }

        public DataServiceMessage<IEnumerable<TopicListDTO>> GetAll()
        {
            List<string> errors = new List<string>();
            bool succeeded = true;
            IEnumerable<TopicListDTO> topicDTOs = null;

            try
            {
                IEnumerable<TopicEntity> topicEntities = unitOfWork.Topics.GetAll();
                topicDTOs = topicEntities.Select(topicEntity =>
                new TopicListDTO
                {
                    Id = topicEntity.Id,
                    CreatorLogin = topicEntity.Creator.ApplicationUser.UserName,
                    DateCreated = topicEntity.DateCreated,
                    DateOfLastMessage = topicEntity.DateOfLastMessage,
                    Title = topicEntity.Title,
                    Description = topicEntity.Description,
                    MessagesCount = topicEntity.Messages.Count
                })
                .OrderByDescending(topic => topic.DateCreated)
                .ToList();
            }
            catch (Exception ex)
            {
                succeeded = false;
                ExceptionMessageBuilder.FillErrors(ex, errors);
            }

            return new DataServiceMessage<IEnumerable<TopicListDTO>>
            {
                Errors = errors,
                Succeeded = succeeded,
                Data = topicDTOs
            };
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        private bool Validate(TopicCreateDTO topic, ICollection<string> errors)
        {
            bool validated = true;

            if (String.IsNullOrEmpty(topic.CreatorLogin))
            {
                validated = false;
                errors.Add("Login of sender is required");
            }
            else if (!unitOfWork.Users.Exists(topic.CreatorLogin))
            {
                validated = false;
                errors.Add("User with such login was not found");
            }

            if (String.IsNullOrEmpty(topic.Title))
            {
                validated = false;
                errors.Add("Title cannot be empty");
            }
            else if (topic.Title.Length > 50)
            {
                validated = false;
                errors.Add("Title must be less than 50 characters");
            }

            if (String.IsNullOrEmpty(topic.Description))
            {
                validated = false;
                errors.Add("Description cannot be empty");
            }
            else if (topic.Description.Length > 200)
            {
                validated = false;
                errors.Add("Description must be less than 200 characters");
            }

            return validated;
        }
    }
}
