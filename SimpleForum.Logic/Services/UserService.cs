using SimpleForum.Data.Contracts;
using SimpleForum.Data.Entities;
using SimpleForum.Logic.Contracts;
using SimpleForum.Logic.DTO.User;
using SimpleForum.Logic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleForum.Logic.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public ServiceMessage Register(UserRegisterDTO user)
        {
            List<string> errors = new List<string>();
            bool succeeded = Validate(user, errors);

            if (succeeded)
            {
                try
                {
                    succeeded = unitOfWork.Accounts.ValidateCredentials(user.Email, user.Login, errors);
                    if (succeeded)
                    {
                        ApplicationUser appUser = new ApplicationUser
                        {
                            UserName = user.Login,
                            Email = user.Email,
                        };

                        succeeded = unitOfWork.Accounts.RegisterUser(appUser, user.Password, errors);
                        if (succeeded)
                        {
                            UserEntity userEntity = new UserEntity
                            {
                                Id = appUser.Id,
                                FirstName = user.FirstName,
                                LastName = user.LastName
                            };

                            unitOfWork.Users.Add(userEntity);
                            unitOfWork.Commit();
                        }
                    }
                }
                catch (Exception ex)
                {
                    succeeded = false;
                    ExceptionMessageBuilder.FillErrors(ex, errors);
                }
            }

            return new ServiceMessage
            {
                Succeeded = succeeded,
                Errors = errors
            };
        }

        private bool Validate(UserRegisterDTO user, ICollection<string> errors)
        {
            bool validated = true;

            if (String.IsNullOrEmpty(user.Email))
            {
                validated = false;
                errors.Add("Incorrect email");
            }
            if (String.IsNullOrEmpty(user.Login))
            {
                validated = false;
                errors.Add("Incorrect login");
            }
            if (String.IsNullOrEmpty(user.Password))
            {
                validated = false;
                errors.Add("Password required");
            }
            if (String.IsNullOrEmpty(user.FirstName))
            {
                validated = false;
                errors.Add("First name required");
            }
            if (String.IsNullOrEmpty(user.LastName))
            {
                validated = false;
                errors.Add("Last name required");
            }

            return validated;
        }

        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}
