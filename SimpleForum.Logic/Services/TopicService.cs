using SimpleForum.Core.Entities;
using SimpleForum.Data.Contracts;
using SimpleForum.Logic.Contracts;
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

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        private bool Validate(TopicCreateDTO topic, List<string> errors)
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
