using SimpleForum.Core.Entities;
using SimpleForum.Data.Contracts;
using SimpleForum.Logic.Contracts;
using SimpleForum.Logic.DTO.Message;
using SimpleForum.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Logic.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork unitOfWork;

        public MessageService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ServiceMessage Create(MessageCreateDTO messageDTO)
        {
            List<string> errors = new List<string>();
            bool succeeded = Validate(messageDTO, errors);

            if (succeeded)
            {
                try
                {
                    UserEntity userEntity = unitOfWork.Users.Get(messageDTO.SenderLogin);
                    if (userEntity != null)
                    {
                        TopicEntity topicEntity = unitOfWork.Topics.Get(messageDTO.TopicId);
                        if (topicEntity != null)
                        {
                            MessageEntity messageEntity = new MessageEntity
                            {
                                Text = messageDTO.Text,
                                Sender = userEntity,
                                Topic = topicEntity,
                                DateCreated = DateTime.Now,
                                DateLastModified = DateTime.Now
                            };

                            unitOfWork.Messages.Add(messageEntity);
                            unitOfWork.Commit();
                        }
                        else
                        {
                            succeeded = false;
                            errors.Add("Topic was not found");
                        }
                    }
                    else
                    {
                        succeeded = false;
                        errors.Add("User was not found");
                    }
                }
                catch (Exception ex)
                {
                    ExceptionMessageBuilder.FillErrors(ex, errors);
                    succeeded = false;
                }
            }

            return new ServiceMessage
            {
                Errors = errors,
                Succeeded = succeeded
            };
        }

        public ServiceMessage Edit(MessageEditDTO messageDTO)
        {
            List<string> errors = new List<string>();
            bool succeeded = Validate(messageDTO, errors);

            if (succeeded)
            {
                try
                {
                    MessageEntity messageEntity = unitOfWork.Messages.Get(messageDTO.Id);
                    messageEntity.Text = messageDTO.Text;
                    messageEntity.DateLastModified = DateTime.Now;

                    unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    ExceptionMessageBuilder.FillErrors(ex, errors);
                    succeeded = false;
                }
            }

            return new ServiceMessage
            {
                Errors = errors,
                Succeeded = succeeded
            };
        }

        public DataServiceMessage<MessageEditDTO> Get(int id)
        {
            List<string> errors = new List<string>();
            bool succeeded = true;
            MessageEditDTO messageDTO = null;

            try
            {
                MessageEntity messageEntity = unitOfWork.Messages.Get(id);
                if (messageEntity != null)
                {
                    messageDTO = new MessageEditDTO
                    {
                        Id = id,
                        Text = messageEntity.Text
                    };
                }
                else
                {
                    succeeded = false;
                    errors.Add("Message not found");
                }
            }
            catch (Exception ex)
            {
                ExceptionMessageBuilder.FillErrors(ex, errors);
                succeeded = false;
            }

            return new DataServiceMessage<MessageEditDTO>
            {
                Errors = errors,
                Succeeded = succeeded,
                Data = messageDTO
            };
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }

        private bool Validate(MessageCreateDTO messageDTO, ICollection<string> errors)
        {
            bool validated = true;

            if (String.IsNullOrEmpty(messageDTO.Text))
            {
                validated = false;
                errors.Add("Text cannot be empty");
            }
            if (String.IsNullOrEmpty(messageDTO.SenderLogin))
            {
                validated = false;
                errors.Add("Message must have an author");
            }

            return validated;
        }

        private bool Validate(MessageEditDTO messageDTO, ICollection<string> errors)
        {
            bool validated = true;

            if (String.IsNullOrEmpty(messageDTO.Text))
            {
                validated = false;
                errors.Add("Text cannot be empty");
            }
            if (messageDTO.Text.Length > 200)
            {
                validated = false;
                errors.Add("Text cannot contain more than 200 characters");
            }

            return validated;
        }
    }
}
